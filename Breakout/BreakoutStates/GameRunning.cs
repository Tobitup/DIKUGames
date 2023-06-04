using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using DIKUArcade.Math;
using System.IO;
using System.Collections.Generic;
using Breakout.Player;
using DIKUArcade.Timers;
using Breakout.Effect;
using Breakout.Levels;
using Breakout.PlayerScore;
using Breakout.PlayerLives;
using Breakout.Blocks;
using Breakout.BallClass;

namespace Breakout.BreakoutStates;

public class GameRunning : IGameState {
    private GameEventBus eventBus;
    private Entity backGroundImage;
    private Player.Player player;
    public Player.Player Player {get { return Player;}}
    private EntityContainer<Ball> ballContainer;
    public EntityContainer<Ball> BallContainer {
        get { return ballContainer; }
        set { ballContainer = value; }
    }
    private EntityContainer<Entity> effectsContainer;
    private bool gameLost;
    public bool GameLost {
        get { return gameLost; }
        set { gameLost = value; }
    }
    private Level currentLevel;
    public Level CurrentLevel {
        get { return currentLevel; }
        set { currentLevel = value; }
    }
    private int numericLevel = 1;
    public int NumericLevel {
        get {return numericLevel;}
        set {numericLevel = value;}
    }
    private LevelLoader levelLoader;
    public LevelLoader LevelLoader {
        get { return levelLoader; }
        set { levelLoader = value; }
    }
    private static GameRunning instance = null;

    private Score levelScore;

    private Lives levelLives;
    public Lives LevelLives {get { return levelLives; }}
    public uint GetCurrentScore = 0;

    /// <summary> Gets the singleton instance of the GameRunning state. </summary>
    /// <returns> The GameRunning instance. </returns>
    public static GameRunning GetInstance() {
        StaticTimer.ResumeTimer();

        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.InitializeGameState();
        }
        return GameRunning.instance;
    }

    /// <summary> Initializes the game state by creating a new player object, loading a level, 
    ///           subscribing to PlayerEvents, and creating the background image entity. </summary>
    /// <returns> Void. </returns>
    private void InitializeGameState() {
        gameLost = false;
        player = new Player.Player(
                            new DynamicShape(new Vec2F(0.4f, 0.1f), new Vec2F(0.22f, 0.025f)),
                            new Image(Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Images",
                                                                                    "player.png")));

        effectsContainer = new EntityContainer<Entity>();
        ballContainer = new EntityContainer<Ball>();

        ballContainer.AddEntity(BallFactory.GenerateNormalBall());

        backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f, 0.0f),
                                        new Vec2F(1.0f, 1.0f)), new Image(Path.Combine(
                                                                LevelLoader.MAIN_PATH, "Assets",
                                                                "Images", "SpaceBackground.png")));
        levelLoader = new LevelLoader(SelectLevel.level1);
        currentLevel = levelLoader.Level;
        eventBus = BreakoutBus.GetBus();
        levelScore = new Score();
        levelLives = new Lives(player.Lives);
        numericLevel = 1;
    }

    /// <summary> Responds to a key press by registering a game event with the 
    ///           appropriate message. </summary>
    /// <param name="key"> A KeyboardKey enum that represents the key that was pressed. </param>
    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Escape:
                eventBus.RegisterEvent(
                                    new GameEvent
                                    {
                                        EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "GAME_PAUSED"
                                    });
                break;
            case KeyboardKey.Left:
                eventBus.RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE_LEFT"
                });
                break;
            case KeyboardKey.Right:
                eventBus.RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE_RIGHT"
                });
                break;
            case KeyboardKey.Up:
                LevelController.incrementLevel();
                break;
        }
    }

    /// <summary> Responds to a key release by registering a game event to stop the given player 
    ///           movement. </summary>
    /// <param name="key"> A KeyboardKey enum that represents the key that was released. </param>
    private void KeyRelease(KeyboardKey key) {
        switch (key){
            case KeyboardKey.Left:
                eventBus.RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE_LEFT_STOP"
                });
                break;
            case KeyboardKey.Right:
                eventBus.RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE_RIGHT_STOP"
                });
                break;
        }
    }

    /// <summary> Handles a keyboard event by invoking either KeyPress() or KeyRelease() method 
    ///           based on the action type. </summary>
    /// <returns> Void. </returns>
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyPress) {
            KeyPress(key);
        }
        if (action == KeyboardAction.KeyRelease) {
            KeyRelease(key);
        }
    }

    /// <summary> Renders the current game state, with background and menu buttons. </summary>
    /// <returns> Void. </returns>
    public void RenderState() {
        backGroundImage.RenderEntity();
        player.Render();
        currentLevel.BlockContainer.RenderEntities();
        currentLevel.Timer.TimerLabel.RenderText();
        levelScore.RenderText();
        ballContainer.RenderEntities();
        levelLives.LifeContainer.RenderEntities();
        effectsContainer.RenderEntities();
    }
    /// <summary> Resets the state of the game paused screen to its initial state. </summary>
    /// <returns> Void. </returns>
    public void ResetState() {
        InitializeGameState();
    }

    /// <summary> Updates the game state by invoking the Move() method and update the players 
    ///           position. </summary>
    /// <returns> Void. </returns>
    public void UpdateState() {
        player.Move();
        CollisionController.IterateCollision(ballContainer,player,currentLevel.BlockContainer);
        EffectController.CollisionEffect(effectsContainer,player);
        BlockController.UpdateBlocks(currentLevel);
        EffectController.UpdateEffects(effectsContainer);
        currentLevel.Timer.UpdateTime();
        BlockController.FindAndRemoveDeadBlocks(currentLevel.BlockContainer,effectsContainer,levelScore);
        GetCurrentScore = levelScore.GetCurrentScore;
        CollisionController.MakeNewBall(ballContainer,levelLives);
        LevelController.LoseIfGameLost();
        levelLives.UpdateLifeContainer();
        LevelController.ChangeLevelIfWon(currentLevel.BlockContainer);
    }
}
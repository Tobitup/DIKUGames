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
using Breakout.Levels;
using Breakout.PlayerScore;
using Breakout.Blocks;

namespace Breakout.BreakoutStates;

public class GameRunning : IGameState, IGameEventProcessor
{
    private GameEventBus eventBus;
    private Entity backGroundImage;
    private Breakout.Player.Player player;
    private Level currentLevel;
    private LevelLoader levelLoader;
    private static GameRunning instance = null;

    private Score levelScore;

    /// <summary> Gets the singleton instance of the GameRunning state. </summary>
    /// <returns> The GameRunning instance. </returns>
    public static GameRunning GetInstance()
    {
        if (GameRunning.instance == null)
        {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.InitializeGameState();
        }
        return GameRunning.instance;
    }

    /// <summary> Initializes the game state by creating a new player object, loading a level, 
    ///           subscribing to PlayerEvents, and creating the background image entity. </summary>
    /// <returns> Void. </returns>
    private void InitializeGameState()
    {
        player = new Player.Player(
                            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.22f, 0.03f)),
                            new Image(Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Images",
                                                                                    "player.png")));

        backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f, 0.0f),
                                        new Vec2F(1.0f, 1.0f)), new Image(Path.Combine(
                                                                LevelLoader.MAIN_PATH, "Assets",
                                                                "Images", "SpaceBackground.png")));
        levelLoader = new LevelLoader(SelectLevel.level4);
        currentLevel = levelLoader.Level;
        eventBus = BreakoutBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, this);

        levelScore = new Score();
    }

    /// <summary> Switches to a new level by setting the current level to the loaded level. 
    /// </summary>
    private void SwitchLevel(SelectLevel newlevel)
    {
        levelLoader = new LevelLoader(newlevel);
        currentLevel = levelLoader.Level;
    }

    /// <summary> Responds to a key press by registering a game event with the 
    ///           appropriate message. </summary>
    /// <param name="key"> A KeyboardKey enum that represents the key that was pressed. </param>
    private void KeyPress(KeyboardKey key)
    {
        switch (key)
        {
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
        }
    }

    /// <summary> Responds to a key release by registering a game event to stop the given player 
    ///           movement. </summary>
        /// <param name="key"> A KeyboardKey enum that represents the key that was released. </param>
    private void KeyRelease(KeyboardKey key)
    {
        switch (key)
        {
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
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
    {
        if (action == KeyboardAction.KeyPress)
        {
            KeyPress(key);
        }
        if (action == KeyboardAction.KeyRelease)
        {
            KeyRelease(key);
        }
    }
    public void MoveMovingBlocks()
    {
        foreach (Block block in currentLevel.BlockContainer)
        {
            {
                block.MoveMoving();
            }
        }
    }

    /// <summary> Renders the current game state, with background and menu buttons. </summary>
    /// <returns> Void. </returns>
    public void RenderState()
    {
        backGroundImage.RenderEntity();
        player.Render();
        currentLevel.BlockContainer.RenderEntities();
        levelScore.RenderText();
    }

    /// <summary> Resets the state of the game paused screen to its initial state. </summary>
    /// <returns> Void. </returns>
    public void ResetState()
    {
        InitializeGameState();
    }

    /// <summary> Updates the game state by invoking the Move() method and update the players 
    ///           position. </summary>
    /// <returns> Void. </returns>
    public void UpdateState()
    {
        player.Move();
        MoveMovingBlocks();
        FindAndRemoveDeadBlocks(currentLevel.BlockContainer);
    }

    /// <summary> Processes a GameEvent by checking its type and message, and performs the 
    ///           subsequent action. </summary>
    /// <param name="gameEvent"> A GameEvent object that represents the event to be processed. 
    /// </param>
    public void ProcessEvent(GameEvent gameEvent)
    {
        if (gameEvent.EventType == GameEventType.PlayerEvent)
        {
            switch (gameEvent.Message)
            {
                case "Test":
                    System.Console.WriteLine("Oh no");
                    break;
            }
        }
    }

    private void FindAndRemoveDeadBlocks(EntityContainer<Block> blocks) {
        foreach (Block block in blocks) {
            if (block.IsDead()) {
                levelScore.IncrementScore(block.Value);
                block.RemoveIfDead();
            }
        }
    }

}
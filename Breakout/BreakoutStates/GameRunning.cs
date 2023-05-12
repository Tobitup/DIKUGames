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
using Breakout.BallClass;

namespace Breakout.BreakoutStates;

public class GameRunning : IGameState, IGameEventProcessor
{
    private GameEventBus eventBus;
    private Entity backGroundImage;
    private Breakout.Player.Player player;
    //private Entity ball;
    private EntityContainer<Ball> ballContainer;
    private IBaseImage ballImage;
    private Level currentLevel;
    private int numericLevel = 1;
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
                            new DynamicShape(new Vec2F(0.4f, 0.1f), new Vec2F(0.22f, 0.025f)),
                            new Image(Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Images",
                                                                                    "player.png")));

        ballContainer = new EntityContainer<Ball>();
        ballImage = new Image(Path.Combine(LevelLoader.MAIN_PATH, "Assets",
                                                                "Images", "ball.png"));
        //ballContainer.AddEntity(new Ball(new Vec2F((0.45f),(0.22f)),ballImage));
        /* ball = new Ball.Ball(new Vec2F((0.45f),(0.22f)),new Image(Path.Combine(
                                                                LevelLoader.MAIN_PATH, "Assets",
                                                                "Images", "ball.png"))); */

        Ball newBall = new Ball(
            new DynamicShape(new Vec2F(0.45f,0.22f), new Vec2F(0.03f, 0.03f), new Vec2F(0.005f, 0.009f) ),
            ballImage
            );
        ballContainer.AddEntity(newBall);

        backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f, 0.0f),
                                        new Vec2F(1.0f, 1.0f)), new Image(Path.Combine(
                                                                LevelLoader.MAIN_PATH, "Assets",
                                                                "Images", "SpaceBackground.png")));
        levelLoader = new LevelLoader(SelectLevel.level1);
        currentLevel = levelLoader.Level;
        eventBus = BreakoutBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, this);

        levelScore = new Score();

        numericLevel = 1;
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
            case KeyboardKey.Up:
                incrementLevel();
                break;
        }
    }

    private void incrementLevel() {
        numericLevel += 1;
        SwitchLevel(LevelTransformer.TransformIntToLevel(numericLevel));
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
    /// <summary> Updates the blocks. </summary>
    /// <returns> Void. </returns>
    public void UpdateBlocks() {
        foreach (IBlock block in currentLevel.BlockContainer) {
            {
                block.Update();
            }
        }
    }

    private void IterateBlocks() {
        ballContainer.Iterate(ball => {
            var activeBall = ball.Shape.AsDynamicShape();
            var ballPlayerDetect = 
                            CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.Shape);

            if (ballPlayerDetect.Collision) {
                // Checks for Horizontal Collision.
                if (ballPlayerDetect.CollisionDir == CollisionDirection.CollisionDirRight ||  
                    ballPlayerDetect.CollisionDir == CollisionDirection.CollisionDirLeft) {
                    var newDirection = activeBall.Direction = new Vec2F(
                        activeBall.Direction.X*(-1),
                        activeBall.Direction.Y);
                    ball.ChangeDirection(newDirection);
                }
                // Checks For Vertical Collision.
                if (ballPlayerDetect.CollisionDir == CollisionDirection.CollisionDirUp || 
                    ballPlayerDetect.CollisionDir == CollisionDirection.CollisionDirDown) {
                    var newDirection = activeBall.Direction = new Vec2F(
                        activeBall.Direction.X,
                        activeBall.Direction.Y*(-1));
                    ball.ChangeDirection(newDirection);
                }
            } else {
                foreach (IBlock block in currentLevel.BlockContainer) {
                //currentLevel.BlockContainer.Iterate(block => {
                    var ballBlockDetect = 
                                CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape);

                    if (ballBlockDetect.Collision) {
                        if (ballBlockDetect.CollisionDir == CollisionDirection.CollisionDirRight || 
                            ballBlockDetect.CollisionDir == CollisionDirection.CollisionDirLeft) {
                        var newDirection = activeBall.Direction = new Vec2F(
                            activeBall.Direction.X*(-1),
                            activeBall.Direction.Y);
                        ball.ChangeDirection(newDirection);
                        block.TakeDamage();
                        block.RemoveIfDead();
                        block.Update();
                    }
                    if (ballBlockDetect.CollisionDir == CollisionDirection.CollisionDirUp || 
                        ballBlockDetect.CollisionDir == CollisionDirection.CollisionDirDown) {
                        var newDirection = activeBall.Direction = new Vec2F(
                            activeBall.Direction.X,
                            activeBall.Direction.Y*(-1));
                        ball.ChangeDirection(newDirection);
                        block.TakeDamage();
                        
                        block.Update();
                    }
                }
            //});
            }
            ball.Move();   
        }});
    }

    /// <summary> Renders the current game state, with background and menu buttons. </summary>
    /// <returns> Void. </returns>
    public void RenderState()
    {
        backGroundImage.RenderEntity();
        player.Render();
        currentLevel.BlockContainer.RenderEntities();
        levelScore.RenderText();
        ballContainer.RenderEntities();
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
        IterateBlocks();
        UpdateBlocks();
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

    private void FindAndRemoveDeadBlocks(EntityContainer<Entity> blocks)
    {
        foreach (IBlock block in currentLevel.BlockContainer)
        {
            if (block.IsDead()) {
                levelScore.IncrementScore(block.Value);
            }
        }

        currentLevel.BlockContainer.Iterate(block => {
            if (block.IsDeleted()) {
                block.DeleteEntity();
            }
        } );
    }

}
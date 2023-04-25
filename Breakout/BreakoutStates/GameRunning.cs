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

namespace Breakout.BreakoutStates;

public class GameRunning : IGameState, IGameEventProcessor {
    private GameEventBus eventBus;
    private Breakout.Player.Player player;
    private List<Image> images;
    private static GameRunning instance = null;
    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.InitializeGameState();
    }
        return GameRunning.instance;
    }

    /// <summary> Initializes all elements required for the game to run </summary>
    /// <returns> Void </returns> 
    private void InitializeGameState() {
        player = new Player.Player(
                            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                            new Image(Path.Combine("Assets", "Images", "player.png")));

        eventBus = BreakoutBus.GetBus();
    
    }

    private void KeyPress(KeyboardKey key) {
        switch(key) {
            case KeyboardKey.Escape:
                BreakoutBus.GetBus().RegisterEvent(
                                    new GameEvent{
                                        EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "GAME_PAUSED"
                                    });
                break;
            case KeyboardKey.Left:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.InputEvent, 
                                                                        Message = "MOVE_LEFT"});
                break;
            case KeyboardKey.Right:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.InputEvent, 
                                                                        Message = "MOVE_RIGHT"});
                break;
            case KeyboardKey.Up:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.InputEvent, 
                                                                        Message = "MOVE_UP"});
                break;
            case KeyboardKey.Down:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.InputEvent, 
                                                                        Message = "MOVE_DOWN"});
                break;
        }
    }
    private void KeyRelease(KeyboardKey key) {
        switch(key){
            case KeyboardKey.Left:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.InputEvent, 
                                                                    Message = "MOVE_LEFT_STOP"});
                break;
            case KeyboardKey.Right:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.InputEvent, 
                                                                    Message = "MOVE_RIGHT_STOP"});
                break;
            case KeyboardKey.Up:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.InputEvent, 
                                                                    Message = "MOVE_UP_STOP"});
                break;
            case KeyboardKey.Down:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.InputEvent, 
                                                                    Message = "MOVE_DOWN_STOP"});
                break;
            }
        }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyPress) {
            KeyPress(key);
        }
        if (action == KeyboardAction.KeyRelease) {
            KeyRelease(key);
        }
    }

    public void RenderState()
    {
        player.Render();
        
    }

    public void ResetState() {
        InitializeGameState();
    }

    public void UpdateState()
    {
     throw new NotImplementedException();
    }

    public void ProcessEvent(GameEvent gameEvent)
    {
        if (gameEvent.EventType == GameEventType.InputEvent) {
            //switch (gameEvent.Message) {
                    throw new NotImplementedException();
        }
    }
}
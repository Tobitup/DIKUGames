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

namespace Breakout.BreakoutStates;

public class GameRunning : IGameState, IGameEventProcessor {
    private GameEventBus eventBus;
    private Entity backGroundImage;
    private Breakout.Player.Player player;
    private List<Image> images;
    private Level currentLevel;
    private LevelLoader levelLoader;

    private static GameRunning instance = null;
    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.InitializeGameState();
    }
        return GameRunning.instance;
    }

    private void InitializeGameState() {
        player = new Player.Player(
                            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.22f, 0.03f)),
                            new Image(Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Images", 
                                                                                    "player.png")));

        levelLoader =  new LevelLoader (SelectLevel.level1);
        currentLevel = levelLoader.Level;
        eventBus = BreakoutBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, this);

        backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f),
                                        new Vec2F(1.0f,1.0f)),new Image(Path.Combine(
                                                                LevelLoader.MAIN_PATH, "Assets",
                                                                "Images", "SpaceBackground.png")));
    
    }
    private void SwitchLevel(SelectLevel newlevel){
        levelLoader = new LevelLoader (newlevel);
        currentLevel = levelLoader.Level;
    }

    private void KeyPress(KeyboardKey key) {
        switch(key) {
            case KeyboardKey.Escape:
                eventBus.RegisterEvent(
                                    new GameEvent{
                                        EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "GAME_PAUSED"
                                    });
                break;
            case KeyboardKey.Left:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, 
                                                                        Message = "MOVE_LEFT"});
                break;
            case KeyboardKey.Right:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, 
                                                                        Message = "MOVE_RIGHT"});                
                break;
        }
    }
    private void KeyRelease(KeyboardKey key) {
        switch(key){
            case KeyboardKey.Left:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, 
                                                                    Message = "MOVE_LEFT_STOP"});
                break;
            case KeyboardKey.Right:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, 
                                                                    Message = "MOVE_RIGHT_STOP"});
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

    public void RenderState() {
        backGroundImage.RenderEntity();
        player.Render();
        currentLevel.BlockContainer.RenderEntities();
    }

    public void ResetState() {
        InitializeGameState();
    }

    public void UpdateState() {
        player.Move();
    }

    public void ProcessEvent(GameEvent gameEvent) {
    if (gameEvent.EventType == GameEventType.PlayerEvent) {
        switch (gameEvent.Message) {
            case "Test":
                System.Console.WriteLine("Oh no");
                break;
            }
        }
    }
}
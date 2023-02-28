using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;

namespace Galaga;
public class Game : DIKUGame , IGameEventProcessor{
    private GameEventBus eventBus;
    private Player player;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        player = new Player(
        new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
        new Image(Path.Combine("Assets", "Images", "Player.png")));

        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);
    }

    private void KeyPress(KeyboardKey key) {
        if (key == KeyboardKey.Escape) {
            window.CloseWindow();
        }
        if (key == KeyboardKey.Left) {
            player.SetMoveLeft(true);
        }
        if (key == KeyboardKey.Right) {
            player.SetMoveRight(true);

        }
        // TODO: switch on key string and set the player's move direction
        }
    private void KeyRelease(KeyboardKey key) {
        // TODO: switch on key string and disable the player's move direction
        
        }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        // TODO: Switch on KeyBoardAction and call proper method
    }
    public void ProcessEvent(GameEvent gameEvent) {
        // Leave this empty for now
    }

    public override void Render() {
        player.Render();
        //throw new System.NotImplementedException("Galaga game has nothing to render yet.");
    }
    public override void Update() {
        window.PollEvents();
        //throw new System.NotImplementedException("Galaga game has no entities to update yet.");
    }
}
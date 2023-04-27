using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using System.IO;
using Breakout.BreakoutStates;
using Breakout.Levels;

namespace Breakout;
public class Game : DIKUGame , IGameEventProcessor{
    private StateMachine stateMachine;
    private GameEventBus eventBus;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        stateMachine = new StateMachine();

        eventBus = BreakoutBus.GetBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.PlayerEvent, 
                                                                GameEventType.WindowEvent,
                                                                GameEventType.GameStateEvent });
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.WindowEvent, this);      
        eventBus.Subscribe(GameEventType.PlayerEvent, this);
        eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);

        LevelLoader level = new LevelLoader(SelectLevel.level1);
        System.Console.WriteLine(level.Level.BlockContainer.CountEntities());
        //System.Console.WriteLine(level.Level.blockContainer);


    }
    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        stateMachine.ActiveState.HandleKeyEvent(action, key);
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent) {
            switch (gameEvent.Message) {
                case "CLOSE_WINDOW":
                    window.CloseWindow();
                    break;
            }
        }
    }

    public override void Render() {
        stateMachine.ActiveState.RenderState();
    }
    public override void Update() {
        window.PollEvents();
        eventBus.ProcessEventsSequentially();
        stateMachine.ActiveState.UpdateState();
    }
}
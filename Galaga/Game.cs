using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using Galaga.GalagaStates;

namespace Galaga;
public class Game : DIKUGame , IGameEventProcessor{
    private GameEventBus eventBus;
    private const int EXPLOSION_LENGTH_MS = 500;
    private StateMachine stateMachine;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        stateMachine = new StateMachine();

        eventBus = GalagaBus.GetBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, 
                                                                GameEventType.WindowEvent,
                                                                GameEventType.GameStateEvent });
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.WindowEvent, this);      
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);
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
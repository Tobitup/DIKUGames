using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using Breakout.BreakoutStates;

namespace Breakout;
public class Game : DIKUGame , IGameEventProcessor{
    private StateMachine stateMachine;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        stateMachine = new StateMachine();

    }
    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
    }

    public void ProcessEvent(GameEvent gameEvent) {
    }

    public override void Render() {
        stateMachine.ActiveState.RenderState();
    }
    public override void Update() {
    }
}
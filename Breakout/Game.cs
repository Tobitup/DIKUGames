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

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        stateMachine = new StateMachine();
        LevelLoader level = new LevelLoader(SelectLevel.level2);
        System.Console.WriteLine(level.Level.BlockContainer.CountEntities());
        //System.Console.WriteLine(level.Level.blockContainer);


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
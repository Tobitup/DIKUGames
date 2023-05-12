using System;
using System.IO;
using Breakout;
using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using DIKUArcade.Input;
using System.Collections.Generic;
using Breakout.BreakoutStates;
using Breakout.Levels;

namespace breakoutTests.TestStateMachine;

[TestFixture]
public class GameRunningTesting {
    private StateMachine stateMachine;
    private GameEventBus eventBus = Breakout.BreakoutBus.GetBus();

    [SetUp]
    public void InitiateStateMachine() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        stateMachine = new StateMachine();
        eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);
        stateMachine.ProcessEvent(
            new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_RUNNING" });
    }

    [Test]
    public void TestInitialState() {
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
    }

    // TODO
    //  - Test for player existing + Check for correct spawn possition
    //  - Test for Space Background being rendered
    //  - Test for GameRunning.cs ProccessEvent (It does nothing currently)
    //  - Test BlockContainer exists
}
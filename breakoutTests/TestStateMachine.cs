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

namespace breakoutTests.TestStateMachine;

[TestFixture]
public class StateMachineTesting {
    private StateMachine stateMachine;

    [SetUp]
    public void InitiateStateMachine() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        stateMachine = new StateMachine();
        Breakout.BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
    }

    [Test]
    public void TestInitialState() {
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }

    [Test]
    public void TestEventGamePaused() {
        stateMachine.ProcessEvent(
        new GameEvent{
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "GAME_PAUSED" });
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
    }

    [Test]
    public void TestEventGameRunning() {
        stateMachine.ProcessEvent(
        new GameEvent{
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = "GAME_RUNNING" });
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameRunning>());
    }
}

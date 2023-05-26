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

namespace breakoutTests.TestGameLost;

[TestFixture]
public class GameLostTesting {
    private StateMachine stateMachine;
    GameLost gameLost = GameLost.GetInstance();
    private GameEventBus eventBus = Breakout.BreakoutBus.GetBus();
    private GameLost menu;

    [SetUp]
    public void InitiateStateMachine() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        stateMachine = new StateMachine();
        eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);
        stateMachine.ProcessEvent(
            new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_LOST" });
        menu = Breakout.BreakoutStates.GameLost.GetInstance();
    }

    [Test]
    public void TestInitialState() {
    /// ASSERT
        gameLost.UpdateState();
        gameLost.RenderState();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameLost>());
    }

    [Test]
    public void TestMenuButtons() {
    /// ARRANGE
        int initialButtonPossition = menu.ActiveMenuButton;
    /// ACT
        menu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
    /// ASSERT
        Assert.That(initialButtonPossition, Is.Not.EqualTo(menu.ActiveMenuButton));
    }

    [Test]
    public void TestAllMenuButtonNavigation() {
    /// ARRANGE
        int initialButtonPossition = menu.ActiveMenuButton;
    /// ACT
        menu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        menu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        menu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
    /// ASSERT
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameLost>());
    }
}
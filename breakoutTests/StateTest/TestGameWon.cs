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

namespace breakoutTests.TestGameWon;

[TestFixture]
public class GameWonTesting {
    private StateMachine stateMachine;
    GameWon gameWon = GameWon.GetInstance();
    private GameEventBus eventBus = Breakout.BreakoutBus.GetBus();
    private GameWon menu;

    [SetUp]
    public void InitiateStateMachine() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        stateMachine = new StateMachine();
        eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);
        stateMachine.ProcessEvent(
            new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_WON" });
        menu = Breakout.BreakoutStates.GameWon.GetInstance();
    }

    [Test]
    public void TestInitialState() {
    /// ASSERT
        gameWon.UpdateState();
        gameWon.RenderState();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameWon>());
    }

    [Test]
    public void TestMenuButtons() {
    /// ARRANGE
        int initialButtonPossition = menu.ActiveMenuButton;
    /// ACT
        menu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        menu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        menu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        menu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        menu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        
    /// ASSERT
        Assert.That(initialButtonPossition, Is.Not.EqualTo(menu.ActiveMenuButton));
    }
}
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

namespace breakoutTests.TestMainMenu;

[TestFixture]
public class MainMenuTesting {
    private StateMachine stateMachine;
    MainMenu mainMenu = MainMenu.GetInstance();
    private GameEventBus eventBus = Breakout.BreakoutBus.GetBus();
    private MainMenu menu;

    [SetUp]
    public void InitiateStateMachine() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        stateMachine = new StateMachine();
        eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);
        menu = Breakout.BreakoutStates.MainMenu.GetInstance();
    }

    [Test]
    public void TestInitialState() {
    /// ASSERT
        mainMenu.UpdateState();
        mainMenu.RenderState();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
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
    public void TestStateChangeToMainMenu() {
        stateMachine.ProcessEvent(
            new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "MAIN_MENU" });
        
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }
}
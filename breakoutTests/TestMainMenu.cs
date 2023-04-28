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
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }

    [Test]
    public void TestMenuButtons() {
        int initialButtonPossition = menu.ActiveMenuButton;
        menu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);

        Assert.That(initialButtonPossition, Is.Not.EqualTo(menu.ActiveMenuButton));
    }
}
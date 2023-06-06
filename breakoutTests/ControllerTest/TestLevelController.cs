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
using Breakout.PlayerLives;

namespace breakoutTests.TestLevelController;

[TestFixture]
public class TestLevelController {
    private StateMachine stateMachine;
    GameRunning gameRunning = GameRunning.GetInstance();
    private GameEventBus eventBus = Breakout.BreakoutBus.GetBus();
    private Lives levelLives;

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
    public void TestSwitchLevel() {
    /// ARRANGE
        var oldLevel = gameRunning.CurrentLevel;
    /// ACT
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress,KeyboardKey.Up);
    /// ASSERT
        Assert.That(gameRunning.CurrentLevel, Is.Not.EqualTo(oldLevel));

    }

    [Test]
    public void TestGameLostStateUpdate() {
    /// ARRANGE
        var gameRunning = GameRunning.GetInstance();
        levelLives = new Lives(3);
        var playerLives = levelLives.GetCurrentLives;
    /// ACT
        for (int i = 0; i < 5; i++) {
            levelLives.LoseLife();
        } if (levelLives.GetCurrentLives == 0) {
            gameRunning.GameLost = true;
            stateMachine.ProcessEvent(
            new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_LOST" });
        }
    /// ASSERT
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GameLost>());
    }
}
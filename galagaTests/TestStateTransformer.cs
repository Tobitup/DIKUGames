using System;
using System.IO;
using Galaga;
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
using Galaga.Squadron;
using Galaga.MovementStrategy;
using Galaga.GalagaStates;

namespace Tests;
[TestFixture]
public class TestStateTransformer {
    [SetUp]
    public void Init() {
    }

    [Test]
    public void TestTransformStatetoString() {
        Assert.AreEqual(GameStateType.MainMenu, StateTransformer.TransformStringToState("MENU"));
        Assert.AreEqual(GameStateType.GamePaused, StateTransformer.TransformStringToState("GAME_PAUSED"));
        Assert.AreEqual(GameStateType.GameRunning, StateTransformer.TransformStringToState("GAME_RUNNING"));
    }

    [Test]
    public void TransformStringtoState() {
        Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.MainMenu), "MENU");
        Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.GamePaused), "GAME_PAUSED");
        Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.GameRunning), "GAME_RUNNING");
    }
}
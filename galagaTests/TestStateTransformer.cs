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
        Assert.That(StateTransformer.TransformStringToState("MENU"), Is.EqualTo(GameStateType.MainMenu));
        Assert.That(StateTransformer.TransformStringToState("GAME_PAUSED"), Is.EqualTo(GameStateType.GamePaused));
        Assert.That(StateTransformer.TransformStringToState("GAME_RUNNING"), Is.EqualTo(GameStateType.GameRunning));
    }

    [Test]
    public void TransformStringtoState() {
        Assert.That(StateTransformer.TransformStateToString(GameStateType.MainMenu), Is.EqualTo("MENU"));
        Assert.That(StateTransformer.TransformStateToString(GameStateType.GamePaused), Is.EqualTo("GAME_PAUSED"));
        Assert.That(StateTransformer.TransformStateToString(GameStateType.GameRunning), Is.EqualTo("GAME_RUNNING"));
    }
}
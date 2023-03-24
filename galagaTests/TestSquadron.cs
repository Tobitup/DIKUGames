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

namespace galagaTests;
[TestFixture]
public class TestSquadron {
    private ISquadron squadron;
    private List<Image> enemyStride;
    private List<Image> alternativeEnemyStride;
    [SetUp]
    public void Init() {
        enemyStride = ImageStride.CreateStrides
                        (4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));

        alternativeEnemyStride = ImageStride.CreateStrides
                        (2, Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png"));
    }

    [Test]
    public void TestSquareSquadron() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        squadron = new SquareSquadron();

        Assert.AreEqual(squadron.MaxEnemies, 4);
    }

    [Test]
    public void TestCrossSquadron() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        squadron = new CrossSquadron();

        Assert.AreEqual(squadron.MaxEnemies, 6);

    }

    [Test]
    public void TestSmileySquadron() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        squadron = new SmileySquadron();

        Assert.AreEqual(squadron.MaxEnemies, 7);

    }
}
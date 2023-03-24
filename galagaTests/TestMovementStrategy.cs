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
public class TestMovementStrategy {
    private List<Image> enemyStride;
    private List<Image> alternativeEnemyStride;
    private ISquadron? squadron;
    private IMovementStrategy? strategy;
    [SetUp]
    public void Init() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        enemyStride = ImageStride.CreateStrides
                        (4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));

        alternativeEnemyStride = ImageStride.CreateStrides
                        (2, Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png"));
    }

    [Test]
    public void TestNoMoveStratagy() {
        squadron = new SquareSquadron();
        strategy = new NoMove();

        strategy.MoveEnemies(squadron.Enemies);
        foreach (Enemy singleEnemy in squadron.Enemies) {
            Assert.That(singleEnemy.Shape.Position.Y, Is.EqualTo(singleEnemy.Startposition.Y));
        }
    }

    [Test]
    public void TestMoveDownStratagy() {
        squadron = new SquareSquadron();
        strategy = new Down();

        strategy.MoveEnemies(squadron.Enemies);
        foreach (Enemy singleEnemy in squadron.Enemies) {
            Assert.That(singleEnemy.Shape.Position.Y, Is.EqualTo(singleEnemy.Startposition.Y));
        }
    }

    [Test]
    public void TestMoveZigZagDownStratagy() {
        squadron = new SquareSquadron();
        strategy = new ZigZagDown();

        strategy.MoveEnemies(squadron.Enemies);
        foreach (Enemy singleEnemy in squadron.Enemies) {
            Assert.That(singleEnemy.Shape.Position.Y, Is.EqualTo(singleEnemy.Startposition.Y));
        }
    }
}
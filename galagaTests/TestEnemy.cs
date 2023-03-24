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
public class TestEnemy {
     private Enemy enemy;


    [SetUp]
    public void Init() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        enemy = new Enemy(
            new DynamicShape(new Vec2F (0.3f, 0.91f), new Vec2F (0.1f , 0.1f)), 
            new Image(Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png")), 
            new Image(Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png")));
    }

    [Test]
    public void TestEnemyTakeDmg() {
        int lastHP = enemy.Hitpoints;
        enemy.TakeDamage();
        Assert.Less(enemy.Hitpoints,lastHP);
    }

    [Test]
    public void TestEnemyCanDie() {
        enemy.TakeDamage();
        enemy.TakeDamage();
        enemy.TakeDamage();
        enemy.TakeDamage();
        enemy.TakeDamage();
        bool shouldBeDead = enemy.IsDead();
        Assert.That(shouldBeDead, Is.EqualTo(true));
    }
}
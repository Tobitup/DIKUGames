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
public class TestHealth {
    private Health health;

    [SetUp]
    public void Init() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        health = new Health(new Vec2F(0.05f,0.4f), new Vec2F(0.3f,0.3f));
    }

    [Test]
    public void TestHealthCanBeLost() {
        Assert.True(health.readHealth == 3);
        health.LoseHealth();
        Assert.Less(health.readHealth,3);
    }
}
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
using Breakout.Player;
using Breakout.Levels;
using Breakout.BallClass;
using Breakout.Effect;
using breakoutTests.TestPlayer;

namespace breakoutTests.EffectTest;

[TestFixture]
    public class LifeUPDownTest {

        [SetUp]
        public void Init() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
        }


        // // BigJim testing
        // [Test]
        // public void TestBigBallInitiated() {
        //     // ARRANGE
        //     Ball BigBall = BallFactory.GenerateNormalBall();
        //     Ball normalBall = BallFactory.GenerateNormalBall();

        //     // ACT
        //     BigBall.InitiateEffect(EffectTransformer.TransformEffectToString(Effects.BigBalls), "START");

        //     // Assert
        //     Assert.That(BigBall.Shape.Extent.X, Is.EqualTo(normalBall.Shape.Extent.X*2.0f));
        // }

}
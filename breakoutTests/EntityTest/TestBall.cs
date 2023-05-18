// Planned release - Next week :)

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

namespace breakoutTests.TestBall;

[TestFixture]
    public class TestBall {
        private EntityContainer<Ball> ballContainer;
        private IBaseImage ballImage;

        [OneTimeSetUp]
        public void Init() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            ballContainer = new EntityContainer<Ball>();
            ballImage = new Image(Path.Combine(LevelLoader.MAIN_PATH, "Assets",
                                                                "Images", "ball.png"));
            Ball newBall = new Ball(
                new DynamicShape(new Vec2F(0.45f,0.22f), 
                                new Vec2F(0.03f, 0.03f), new Vec2F(0.000f, 0.009f) ), ballImage);
            ballContainer.AddEntity(newBall);
        }

        [Test]
        public void TestBallInitilized() {
        /// ARRANGE
            int ballCount;
        /// ACT
            ballCount = ballContainer.CountEntities();
        /// ASSERT
            Assert.That(ballCount, Is.EqualTo(1));
        }

        [Test]
        public void TestBallCantLeaveWindowUp() {
        /// ARRANGE
            ballContainer.Iterate(ball => {
            ball.Shape.Position = new Vec2F(0.5f,0.9f);

        /// ACT
            for (int i = 0; i < 1000; i++) {
                ball.Move();
            }
        /// ASSERT
            Assert.That(ball.Shape.Position.Y, Is.EqualTo(1f));
        });

    }
}
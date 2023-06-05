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
        private EntityContainer<Ball> ballContainerRandom;

        [OneTimeSetUp]
        public void Init() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            ballContainer = new EntityContainer<Ball>();
            ballContainerRandom = new EntityContainer<Ball>();
            ballContainer.AddEntity(BallFactory.GenerateNormalBall());
            ballContainerRandom.AddEntity(BallFactory.GenerateSemiRandomDirBall
                                                        (new Vec2F(0.5f,0.5f)));
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
            for (int i = 0; i < 15; i++) {
                CollisionController.Move(ball);
            }
        /// ASSERT
            Assert.Less(ball.Shape.Position.Y , 1.0f);
        });
    }

    [Test]
        public void TestBallCantLeaveWindowLeft() {
        /// ARRANGE
            ballContainer.Iterate(ball => {
            ball.Shape.Position = new Vec2F(0.5f,0.5f);
        /// ACT
            for (int i = 0; i < 500; i++) {
                CollisionController.Move(ball);
            }
        /// ASSERT
            Assert.Greater(ball.Shape.Position.X , -1.0f);
        });
    }

    [Test]
        public void TestBallCantLeaveWindowRight() {
        /// ARRANGE
            ballContainer.Iterate(ball => {
            ball.Shape.Position = new Vec2F(0.9f,0.5f);
        /// ACT
            for (int i = 0; i < 500; i++) {
                CollisionController.Move(ball);
            }
        /// ASSERT
            Assert.Less(ball.Shape.Position.X , 1.0f);
        });
    }

    [Test]
        public void TestBallCanDie() {
        /// ARRANGE
            ballContainer.Iterate(ball => {
            ball.Shape.Position = new Vec2F(0.5f,0.9f);
        /// ACT
            for (int i = 0; i < 500; i++) {
                CollisionController.Move(ball);
            }
        /// ASSERT
            Assert.That(ball.IsBallDead() ,Is.EqualTo(true));
        });
    }

    [Test]
        public void TestBallDirUp() {
        /// ARRANGE
            ballContainer.Iterate(ball => {
            ball.Shape.Position = new Vec2F(0.5f,0.2f);
            var ballYDir = ball.Shape.AsDynamicShape().Direction.Y;
        /// ACT
            BallMath.DirUp(ball , ball.Shape.Position , new Vec2F(0.022f,0.025f));
        /// ASSERT
            Assert.Less(ballYDir , -ball.Shape.AsDynamicShape().Direction.Y);
        });
    }
}
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
public class BigBallsTesting {
    Vec2F Effectsize;
    Vec2F Effectpos;
    DynamicShape EffectShape;
    IBaseImage Effectimage;

    Vec2F PlayerSize;
    Vec2F PlayerPos;
    DynamicShape PlayerShape;
    IBaseImage PlayerImage;


    [SetUp]
    public void Init() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        Effectsize = new Vec2F(0.0f, 0.0f);
        Effectpos = new Vec2F(0.0f, 0.0f);
        EffectShape = new DynamicShape(Effectsize, Effectpos);
        Effectimage = new Image(Path.Combine(LevelLoader.MAIN_PATH,"Assets", "Images",
                                EffectTransformer.TransformEffectToPath(Effects.BigJim)));
        PlayerPos = new Vec2F(0.4f, 0.1f);
        PlayerSize = new Vec2F(0.22f, 0.025f);
        PlayerShape = new DynamicShape(PlayerPos, PlayerSize);
        PlayerImage = new Image(Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Images",
                                                                                "player.png"));
    }


    // BigJim testing
    [Test]
    public void TestBigBallInitiated() {
        // ARRANGE
        Ball BigBall = BallFactory.GenerateNormalBall();
        Ball normalBall = BallFactory.GenerateNormalBall();
        // ACT
        BigBall.InitiateEffect(EffectTransformer.TransformEffectToString(Effects.BigBalls),"START");
        // Assert
        Assert.That(BigBall.Shape.Extent.X, Is.EqualTo(normalBall.Shape.Extent.X*2.0f));
    }

    [Test]
    public void TestBigBallNotStacking() {
        // ARRANGE
        Ball BigBall = BallFactory.GenerateNormalBall();
        Ball normalBall = BallFactory.GenerateNormalBall();
        // ACT
        BigBall.InitiateEffect(EffectTransformer.TransformEffectToString(Effects.BigBalls),"START");
        BigBall.InitiateEffect(EffectTransformer.TransformEffectToString(Effects.BigBalls),"START");
        // Assert
        Assert.That(BigBall.Shape.Extent.X, Is.EqualTo(normalBall.Shape.Extent.X*2.0f));
    }

    [Test]
    public void TestBigBallReverse() {
        // ARRANGE
        Ball BigBall = BallFactory.GenerateNormalBall();
        Ball normalBall = BallFactory.GenerateNormalBall();
        // ACT
        BigBall.InitiateEffect(EffectTransformer.TransformEffectToString(Effects.BigBalls),"START");
        BigBall.InitiateEffect(EffectTransformer.TransformEffectToString(Effects.BigBalls),"STOP");
        // Assert
        Assert.That(BigBall.Shape.Extent.X, Is.EqualTo(normalBall.Shape.Extent.X));
    }

    [Test]
    public void TestBigBallReverseNotStacking() {
        // ARRANGE
        Ball BigBall = BallFactory.GenerateNormalBall();
        Ball normalBall = BallFactory.GenerateNormalBall();
        // ACT
        BigBall.InitiateEffect(EffectTransformer.TransformEffectToString(Effects.BigBalls),"START");
        BigBall.InitiateEffect(EffectTransformer.TransformEffectToString(Effects.BigBalls), "STOP");
        BigBall.InitiateEffect(EffectTransformer.TransformEffectToString(Effects.BigBalls), "STOP");
        // Assert
        Assert.That(BigBall.Shape.Extent.X, Is.EqualTo(normalBall.Shape.Extent.X));
    }
}
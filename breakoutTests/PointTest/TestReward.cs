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
using Breakout.PlayerScore;


namespace breakoutTests.TestRewards;

[TestFixture]
public class TestReward {

    [Test]
    public void TestStartingScore() {
    /// ARANGE
        Score score = new Score();

    /// ACT
    
    /// ASSERT
        Assert.That(score.GetCurrentScore, Is.EqualTo((uint) 0));
    }


    [TestCase((uint)1, (uint)1)]
    [TestCase((uint)4, (uint)4)]
    [TestCase((uint)100, (uint)100)]
    public void TestIncrementingScore(uint value, uint expectedScore) {
    /// ARANGE
        Score score = new Score();

    /// ACT
        score.IncrementScore(value);

    /// ASSERT
        Assert.That(score.GetCurrentScore, Is.EqualTo(expectedScore));
    }

    [Test]
    public void TestScoreReset() {
    /// ARANGE
        Score score = new Score();

    /// ACT
        score.IncrementScore((uint) 5);
        score.ResetScore();

    /// ASSERT
        Assert.That(score.GetCurrentScore, Is.EqualTo((uint) 0));
    }
}

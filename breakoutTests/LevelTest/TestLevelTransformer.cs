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
using Breakout.Levels;

namespace breakoutTests.TestLevelTransformer;

[TestFixture]
public class LevelTransformerTesting {

    [SetUp]
    public void SetUp() { }


    [Test]
    public void TestLevel1() {
    /// ARRANGE

    /// ACT
        var level1ToString = LevelTransformer.TransformLevelToString(SelectLevel.level1);
        var level1IntToLvl = LevelTransformer.TransformIntToLevel(1);

    /// ASSERT
        Assert.That(level1ToString, Is.EqualTo("level1.txt"));
        Assert.That(level1IntToLvl, Is.EqualTo(SelectLevel.level1));
    }


    [Test]
    public void TestLevel2() {
    /// ARRANGE

    /// ACT
        var level2ToString = LevelTransformer.TransformLevelToString(SelectLevel.level2);
        var level2IntToLvl = LevelTransformer.TransformIntToLevel(2);

    /// ASSERT
        Assert.That(level2ToString, Is.EqualTo("level2.txt"));
        Assert.That(level2IntToLvl, Is.EqualTo(SelectLevel.level2));
    }


    [Test]
    public void TestLevel3() {
    /// ARRANGE

    /// ACT
        var level3ToString = LevelTransformer.TransformLevelToString(SelectLevel.level3);
        var level3IntToLvl = LevelTransformer.TransformIntToLevel(3);

    /// ASSERT
        Assert.That(level3ToString, Is.EqualTo("level3.txt"));
        Assert.That(level3IntToLvl, Is.EqualTo(SelectLevel.level3));
    }


    [Test]
    public void TestLevel4() {
    /// ARRANGE

    /// ACT
        var level4ToString = LevelTransformer.TransformLevelToString(SelectLevel.level4);
        var level4IntToLvl = LevelTransformer.TransformIntToLevel(4);

    /// ASSERT
        Assert.That(level4ToString, Is.EqualTo("level4.txt"));
        Assert.That(level4IntToLvl, Is.EqualTo(SelectLevel.level4));
    }
}

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

namespace breakoutTests.TestLevels;

[TestFixture]
public class TestLevelParser {

    [SetUp]
    public void SetUp() {

    }

    [Test]
    public void TestCorrectLegendData() {
    /// ACT
    string[] readData = FileReader.ReadFile(
                                    Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Levels",
                                                                            "Level1.txt"));

    LevelParser levelParser = new LevelParser(readData);

    Dictionary<string, string> expectedLegendData;
    expectedLegendData.Add("%", "blue-block.png");
    expectedLegendData.Add("0", "grey-block.png");
    expectedLegendData.Add("1", "orange-block.png");
    expectedLegendData.Add("a", "purple-block.png");

    Dictionary<string, string> legendData = levelParser.parseLegendData();

    /// ASSERT
    Assert.That(legendData, Is.EqualTo(expectedLegendData));
    }

    [Test]
    public void TestCorrectMetaData() {
    /// ACT
    string[] readData = FileReader.ReadFile(
                                    Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Levels",
                                                                            "Level1.txt"));

    LevelParser levelParser = new LevelParser(readData);

    Dictionary<string, string> expectedMetaData;
    expectedLegendData.Add("Name", "LEVEL 1");
    expectedLegendData.Add("Time", "300");
    expectedLegendData.Add("Hardened", "#");
    expectedLegendData.Add("PowerUp", "2");

    Dictionary<string, string> metaData = levelParser.parseMetaData();

    /// ASSERT
    Assert.That(legendData, Is.EqualTo(expectedMetaData));
    }

    [Test]
    public void TestCorrectMetaData() {
    /// ACT
    string[] readData = FileReader.ReadFile(
                                    Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Levels",
                                                                            "Level1.txt"));

    LevelParser levelParser = new LevelParser(readData);

    Dictionary<string, string> expectedMetaData;
    expectedLegendData.Add("Name", "LEVEL 1");
    expectedLegendData.Add("Time", "300");
    expectedLegendData.Add("Hardened", "#");
    expectedLegendData.Add("PowerUp", "2");

    Dictionary<string, string> metaData = levelParser.parseMetaData();

    /// ASSERT
    Assert.That(legendData, Is.EqualTo(expectedMetaData));
    }
}


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
    /// ARRANGE
    string[] readData = FileReader.ReadFile(
                                    Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Levels",
                                                                            "Level1.txt"));
    LevelParser levelParser = new LevelParser(readData);
    Dictionary<string, string> expectedLegendData = new Dictionary<string, string>();

    /// ACT
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
    /// ARRANGE
    string[] readData = FileReader.ReadFile(
                        Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Levels",
                                                                 "Level1.txt"));
    LevelParser levelParser = new LevelParser(readData);
    Dictionary<string, string> expectedMetaData = new Dictionary<string, string>();

    /// ACT
    expectedMetaData.Add("Name", "LEVEL 1");
    expectedMetaData.Add("Time", "300");
    expectedMetaData.Add("Hardened", "#");
    expectedMetaData.Add("PowerUp", "2");
    Dictionary<string, string> metaData = levelParser.parseMetaData();

    /// ASSERT
    Assert.That(metaData, Is.EqualTo(expectedMetaData));
    }

    [TestCase(2,2, "a")]
    [TestCase(6,5, "%")]
    [TestCase(6,6, "1")]
    [TestCase(3,6, "0")]
    [TestCase(0,24, "-")]
    [TestCase(11,24, "-")]

    public void TestCorrectMapData(int x, int y, string expectedChar) {
        /// ARRANGE
        string[] readData = FileReader.ReadFile(
                            Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Levels",
                                                                    "Level1.txt"));
        LevelParser levelParser = new LevelParser(readData);

        string[,] mapData = levelParser.parseLevelMap();

        /// ASSERT
        Assert.That(mapData[y,x], Is.EqualTo(expectedChar));
    }

    [Test]
    public void TestWrongFileReturnsEmptyMapData() {
        /// ARRANGE
        string[] readData = FileReader.ReadFile(
                            Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Levels",
                                                                    "Leve1.txt"));
        LevelParser levelParser = new LevelParser(readData);

        string[,] mapData = levelParser.parseLevelMap();

        /// ASSERT
        for (int i = 0; i < 25; i++) {
            for (int j = 0; j < 12; j++) {
                Assert.That(mapData[i,j], Is.EqualTo("-"));
            }
        }
    }
}


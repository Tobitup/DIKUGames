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


    // Test that the right input in ReadFile will result in a string array
    [Test]
    public void TestReadFile() {
    /// ACT
        string[] testValue = FileReader.ReadFile(
                                    Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Levels",
                                                                            "Level1.txt"));
    /// ASSERT
        Assert.That(testValue.GetType(), Is.EqualTo(typeof(string[])));
    }

    // Test that the wrong file input in ReadFile will result in empty string array
    [Test]
    public void ReadFile_ThrowsFileNotFoundException() {
    /// ACT
        string invalidFilePath = Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Levels",
                                                                            "Level8.txt");
    /// ASSERT
        Assert.That(FileReader.ReadFile(invalidFilePath), Is.EqualTo(new string[0]));
    }



    /// TEST invalid files

    /// EXAMPLE TESTING FOR READING A SPECIFIC LEVEL
    [Test]
    public void ReadFile_ThrowsFileNotFoundException() {
    /// ACT
    string invalidFilePath = Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Levels",
                                                                            "Level8.txt");

    /// ASSERT
    Assert.That(FileReader.ReadFile(invalidFilePath), Is.EqualTo(new string[0]));
    }



}

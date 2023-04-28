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
public class LevelTesting {

    [SetUp]
    public void SetUp() {

    }
    //Test that the right input in ReadFile will result in a string array
    [Test]
    public void TestReadFile() {
    string[] testValue = FileReader.ReadFile(
                                    Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Levels",
                                    "Level1.txt"));
    Assert.AreEqual(testValue.GetType(), typeof(string[]));

    }
    //Test that the wrong file input in ReadFile will result in empty string array
    [Test]
    public void ReadFile_ThrowsFileNotFoundException() {
        string invalidFilePath = Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Levels",
                                                                                    "Level8.txt");
        Assert.AreEqual(FileReader.ReadFile(invalidFilePath), new string[0]);
    }   
}

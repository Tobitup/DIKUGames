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
using breakout.Blocks;

namespace breakoutTests.TestBlocks;

[TestFixture]
public class TestBlocks
{
    private EntityContainer<Entity> blockContainer = new EntityContainer<Entity>();
    private GameEventBus eventBus = Breakout.BreakoutBus.GetBus();
    private bool isBusInitialized;

    [SetUp]
    public void Init()
    {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        // Condition check to assure a BreakoutBus is always initialized.
        if (!isBusInitilized)
        {
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.PlayerEvent,
                                    GameEventType.WindowEvent, GameEventType.GameStateEvent });
            isBusInitialized = true;
        }
        Entity unbreaky = BlockFactory.CreateNewBlock("Unbreakable");
        Entity movie = BlockFactory.CreateNewBlock("Moving");
        Entity Normal = BlockFactory.CreateNewBlock("Normal");

    }


    [Test]
    public void TestMovingBlockMoves()
    {
        //check that movign blocks change position after calling update
        Assert.pass();
    }
    public void TestUnbreakableNoBreak()
    {
        //iterate thru blockcontainer to see that it doesnt take damage
        Assert.pass();
    }
    public void TestifRemoveifdead()
    {
        //iterate thru blockcontainer to see that it dies
        Assert.pass();
    }

}
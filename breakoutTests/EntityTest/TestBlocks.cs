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
using Breakout.Blocks;

namespace breakoutTests.TestBlocks;

[TestFixture]
    public class TestBlocks {
        
        private GameEventBus eventBus = Breakout.BreakoutBus.GetBus();

       // [SetUp]
        

        [Test]
        public void TestNormalBlockInizialized() {
        /// ARRANGE
           
        /// ACT
           
        /// ASSERT
            
        }

         [Test]
        public void TestNormalBlockTakeDmg() {
        /// ARRANGE
           
        /// ACT
           
        /// ASSERT
            
        }
         [Test]
        public void TestNormalBlockDie() {
        /// ARRANGE
           
        /// ACT
           
        /// ASSERT
            
        }

         [Test]
        public void TestMovingBlockInizialized() {
        /// ARRANGE
           
        /// ACT
           
        /// ASSERT
            
        }
         [Test]
        public void TestMovingBlockTakeDmg() {
        /// ARRANGE
           
        /// ACT
           
        /// ASSERT
            
        }
         [Test]
        public void TestMovingBlockDie() {
        /// ARRANGE
           
        /// ACT
           
        /// ASSERT
            
        }

         [Test]
        public void TestUnbreakableBlockInizialized() {
        /// ARRANGE
           
        /// ACT
           
        /// ASSERT
            
        }

         [Test]
        public void TestUnbreakableBlock() {
        /// ARRANGE
           
        /// ACT
           
        /// ASSERT
            
        }
    }

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
        private EntityContainer<Entity> normalBlockContainer = new EntityContainer<Entity>();
        private EntityContainer<Entity> unbreakableBlockContainer = new EntityContainer<Entity>();
        private EntityContainer<Entity> movingBlockContainer = new EntityContainer<Entity>();
        private EntityContainer<Entity> powerUpBlockContainer = new EntityContainer<Entity>();
        private EntityContainer<Entity> fakeBlockContainer = new EntityContainer<Entity>();

        [OneTimeSetUp]
        public void Init() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            string imagePath = Path.Combine
                    (LevelLoader.MAIN_PATH, "Assets", "Images", "blue-block.png");
            string normalBlockType = "Normal";
            string unbreakableBlockType = "Unbreakable";
            string movingBlockType = "Moving";
            string powerUpBlockType = "PowerUp";
            string falseStringBlockType = "I have of late but wherefore I know not lost all my mirth";
    
            normalBlockContainer.AddEntity(BlockFactory.CreateNewBlock
                    (normalBlockType, new Vec2I(0, 0), new Image(imagePath)));

            unbreakableBlockContainer.AddEntity(BlockFactory.CreateNewBlock
                    (unbreakableBlockType, new Vec2I(0, 1), new Image(imagePath)));
            
            movingBlockContainer.AddEntity(BlockFactory.CreateNewBlock
                    (movingBlockType, new Vec2I(1, 0), new Image(imagePath)));

            powerUpBlockContainer.AddEntity(BlockFactory.CreateNewBlock
                    (powerUpBlockType, new Vec2I(1, 0), new Image(imagePath)));

            fakeBlockContainer.AddEntity(BlockFactory.CreateNewBlock
                    (falseStringBlockType, new Vec2I(1, 0), new Image(imagePath)));
        }


        [Test]
        public void TestNormalBlockInizialized() {
        /// ARRANGE
            int blockCount;
        /// ACT
            blockCount = normalBlockContainer.CountEntities();
        /// ASSERT
            Assert.That(blockCount, Is.EqualTo(1));
        }

        [Test]
        public void TestNormalBlockTakeDmg() {
        /// ARRANGE
            int currentBlockHealth;
        /// ACT
           
        /// ASSERT
            foreach (IBlock block in normalBlockContainer) {
                currentBlockHealth = block.HitPoints;
                block.TakeDamage();
                block.Update();
                Assert.Less(block.HitPoints, currentBlockHealth);
           }
        }

        [Test]
        public void TestNormalBlockDie() {
        /// ARRANGE

        /// ACT
           
        /// ASSERT
            foreach (IBlock block in normalBlockContainer) {
                block.TakeDamage();
                block.TakeDamage();
                block.TakeDamage();
                Assert.That(block.IsDead(),Is.EqualTo(true));
           }
        }

        [Test]
        public void TestMovingBlockInizialized() {
       /// ARRANGE
            int blockCount;
        /// ACT
            blockCount = movingBlockContainer.CountEntities();
        /// ASSERT
            Assert.That(blockCount, Is.EqualTo(1));  
        }
        
        [Test]
        public void TestMovingBlockTakeDmg() {
        /// ARRANGE
            int currentBlockHealth;
        /// ACT
           
        /// ASSERT
            foreach (IBlock block in movingBlockContainer) {
                currentBlockHealth = block.HitPoints;
                block.TakeDamage();
                Assert.Less(block.HitPoints, currentBlockHealth);
           }
        }

        [Test]
        public void TestMovingBlockDie() {
        /// ARRANGE
           
        /// ACT
           
        /// ASSERT
           foreach (IBlock block in movingBlockContainer) {
                block.TakeDamage();
                block.TakeDamage();
                block.TakeDamage();
                Assert.That(block.IsDead(),Is.EqualTo(true));
           } 
        }

        [Test]
        public void TestMovingBlockMoving() {
        /// ARRANGE
           
        /// ACT
                
        /// ASSERT
            foreach (IBlock block in movingBlockContainer) {
                var blockPos = block.Shape.Position.X;
                for (int i = 0; i < 20000; i++) {
                    block.Update();
                }
                Assert.That(blockPos, Is.Not.EqualTo(block.Shape.Position.X));
           } 
        }

         [Test]
        public void TestUnbreakableBlockInizialized() {
        /// ARRANGE
            int blockCount;
        /// ACT
            blockCount = unbreakableBlockContainer.CountEntities();
        /// ASSERT
            Assert.That(blockCount, Is.EqualTo(1));
            
        }

        [Test]
        public void TestUnbreakableBlockTakeDmg() {
        /// ARRANGE
            int currentBlockHealth;
        /// ACT
           
        /// ASSERT
            foreach (IBlock block in unbreakableBlockContainer) {
                currentBlockHealth = block.HitPoints;
                block.TakeDamage();
                block.Update();
                Assert.That(block.HitPoints, Is.EqualTo(currentBlockHealth));
           }
            
        }

        [Test]
        public void TestUnbreakableBlockDie() {
        /// ARRANGE
           
        /// ACT
           
        /// ASSERT
           foreach (IBlock block in unbreakableBlockContainer) {
                block.TakeDamage();
                block.TakeDamage();
                block.TakeDamage();
                Assert.That(block.IsDead(),Is.EqualTo(false));
           } 
        }

        [Test]
        public void TestPowerUpBlockInizialized() {
        /// ARRANGE
            int blockCount;
        /// ACT
            blockCount =  powerUpBlockContainer.CountEntities();
        /// ASSERT
            Assert.That(blockCount, Is.EqualTo(1));
        }

        [Test]
        public void TestPowerUpBlockTakeDmg() {
        /// ARRANGE
            int currentBlockHealth;
        /// ACT
           
        /// ASSERT
            foreach (IBlock block in powerUpBlockContainer) {
                currentBlockHealth = block.HitPoints;
                block.TakeDamage();
                Assert.Less(block.HitPoints, currentBlockHealth);
           }
        }

        [Test]
        public void TestPowerUpBlockDie() {
        /// ARRANGE

        /// ACT
           
        /// ASSERT
            foreach (IBlock block in powerUpBlockContainer) {
                block.TakeDamage();
                block.TakeDamage();
                block.TakeDamage();
                block.Update();
                Assert.That(block.IsDead(),Is.EqualTo(true));
           }
        }
    }

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
using Breakout.PlayerLives;

namespace breakoutTests.TestPlayer;

[TestFixture]
    public class TestPlayer {
        private Player player;
        private GameEventBus eventBus = Breakout.BreakoutBus.GetBus();
        private bool isBusInitilized;
        private Lives levelLives;

        [SetUp]
        public void Init() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            // Condition check, to assure a BreakoutBus is always initilized.
            if (!isBusInitilized) {
                eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.PlayerEvent, 
                                    GameEventType.WindowEvent, GameEventType.GameStateEvent });
                isBusInitilized = true;
            }

            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine(LevelLoader.MAIN_PATH,"Assets", "Images", "player.png")));
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
        }

        
        [Test]
        public void TestPlayerMoveLeft() {
        /// ARRANGE
            Vec2F initialPos = player.Shape.Position;
            eventBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.PlayerEvent,
                Message = "MOVE_LEFT" });
        /// ACT
            for(int i = 0; i <100; i++) {
                Breakout.BreakoutBus.GetBus().ProcessEvents();
                player.Move();
            }
            eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE_LEFT_STOP" });
        /// ASSERT
            Assert.That(initialPos.X, Is.Not.EqualTo(player.Shape.Position.X));
            Assert.Less((player.Shape.Position.X), (initialPos.X));
        }

        [Test]
        public void TestPlayerMoveRight() {
        /// ARRANGE
            Vec2F initialPos = player.Shape.Position;
            eventBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.PlayerEvent,
                Message = "MOVE_RIGHT" });
        /// ACT
            for(int i = 0; i <100; i++) {
                Breakout.BreakoutBus.GetBus().ProcessEvents();
                player.Move();
            }
            eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE_RIGHT_STOP" });
        /// ASSERT
            Assert.That(initialPos.X, Is.Not.EqualTo(player.Shape.Position.X));
            Assert.Greater((player.Shape.Position.X), (initialPos.X));
        }

        [Test]
        public void TestPlayerOutOfBoundLeft() {
        /// ARRANGE
            Vec2F initialPos = player.Shape.Position;
            eventBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.PlayerEvent,
                Message = "MOVE_LEFT" });
        /// ACT
            for(int i = 0; i <10000; i++) {
                Breakout.BreakoutBus.GetBus().ProcessEvents();
                player.Move();
            }
            eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE_LEFT_STOP" });
        /// ASSERT
            Assert.IsTrue(initialPos.X >= 0.0f);
        }

        [Test]
        public void TestPlayerOutOfBoundRight() {
        /// ARRANGE
            Vec2F initialPos = player.Shape.Position;
            eventBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.PlayerEvent,
                Message = "MOVE_RIGHT" });
        /// ACT
            for(int i = 0; i <10000; i++) {
                Breakout.BreakoutBus.GetBus().ProcessEvents();
                player.Move(); 
            }
            eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE_RIGHT_STOP" });
        /// ASSERT
            Assert.IsTrue(initialPos.X <= 1.0f);
        }

        [Test]
        public void TestAddingLife() {
        /// ARRANGE
            levelLives = new Lives(3);
            var expectedLives = 4;
        /// ACT
            levelLives.AddLife();
        /// ASSERT
            Assert.That(levelLives.GetCurrentLives,Is.EqualTo(expectedLives));
        }

        [Test]
        public void TestResetingLives() {
        /// ARRANGE
            levelLives = new Lives(3);
            var expectedLives = 0;
        /// ACT
            levelLives.ResetLife();
        /// ASSERT
            Assert.That(levelLives.GetCurrentLives,Is.EqualTo(expectedLives));
        }
}
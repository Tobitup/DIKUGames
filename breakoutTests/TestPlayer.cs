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

namespace breakoutTests.TestPlayer;

    [TestFixture]
    public class TestPlayer {
        private Player player;
        private GameEventBus eventBus = Breakout.BreakoutBus.GetBus();
        //private StateMachine stateMachine;
        private bool isBusInitilized;

        [SetUp]
        public void Init() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            // Condition check to assure a BreakoutBus is always initilized.
            if (!isBusInitilized) {
                eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.PlayerEvent, 
                                    GameEventType.WindowEvent, GameEventType.GameStateEvent });
                isBusInitilized = true;
            }

            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("..","Breakout","Assets", "Images", "Player.png")));

            eventBus.Subscribe(GameEventType.PlayerEvent, player);
            //eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);

        }
        [Test]
        public void TestPlayerMoveLeft() {
            Vec2F initialPos = player.Shape.Position;
            eventBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.PlayerEvent,
                Message = "MOVE_LEFT"
                });

            for(int i = 0; i <100; i++) {
                Breakout.BreakoutBus.GetBus().ProcessEvents();
                player.Move();
            }

            eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE_LEFT_STOP"
                });
            
            Assert.That(initialPos.X, Is.Not.EqualTo(player.Shape.Position.X));
        }

        [Test]
        public void TestPlayerMoveRight() {
            Vec2F initialPos = player.Shape.Position;
            eventBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.PlayerEvent,
                Message = "MOVE_RIGHT"
                });

            for(int i = 0; i <100; i++) {
                Breakout.BreakoutBus.GetBus().ProcessEvents();
                player.Move();
            }

            eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE_RIGHT_STOP"
                });
            
            Assert.That(initialPos.X, Is.Not.EqualTo(player.Shape.Position.X));
        }

        [Test]
        public void TestPlayerOutOfBoundLeft() {
            Vec2F initialPos = player.Shape.Position;
            eventBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.PlayerEvent,
                Message = "MOVE_LEFT"
                });
            for(int i = 0; i <10000; i++) {
                Breakout.BreakoutBus.GetBus().ProcessEvents();
                player.Move();
            }

            eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE_LEFT_STOP"
                });
            
            Assert.IsTrue(initialPos.X >= 0.0f);
        }

        [Test]
        public void TestPlayerOutOfBoundRight() {
            Vec2F initialPos = player.Shape.Position;
            eventBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.PlayerEvent,
                Message = "MOVE_RIGHT"
                });
            for(int i = 0; i <10000; i++) {
                Breakout.BreakoutBus.GetBus().ProcessEvents();
                player.Move();
            }

            eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "MOVE_RIGHT_STOP"
                });
            
            Assert.IsTrue(initialPos.X <= 1.0f);
        }
}
using System;
using System.IO;
using Galaga;
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
using Galaga.Squadron;
using Galaga.MovementStrategy;
using Galaga.GalagaStates;

namespace galagaTests;
    [TestFixture]
    public class TestPlayer {
        private Player player;
        private GameEventBus eventBus = GalagaBus.GetBus();
        private StateMachine stateMachine;
        private bool BusFound;

        [SetUp]
        public void Init() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            if (!BusFound) {
                eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, 
                                    GameEventType.WindowEvent, GameEventType.GameStateEvent });
                BusFound = true;
            }

            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("..","Galaga","Assets", "Images", "Player.png")));

            eventBus.Subscribe(GameEventType.InputEvent, player);

        }
        [Test]
        public void TestPlayerMoveLeft() {
            Vec2F firstPos = player.GetPosition();
            eventBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.InputEvent,
                Message = "MOVE_LEFT"
                });

            GalagaBus.GetBus().ProcessEvents();
            player.Move();

            System.Threading.Thread.Sleep(1000);
            eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.InputEvent,
                    Message = "MOVE_LEFT_STOP"
                });
                
            Assert.AreNotEqual(firstPos.X, player.GetPosition().X);
        }

        [Test]
        public void TestPlayerMoveRight() {
            Vec2F firstPos = player.GetPosition();
            eventBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.InputEvent,
                Message = "MOVE_RIGHT"
                });
                
            GalagaBus.GetBus().ProcessEvents();
            player.Move();

            System.Threading.Thread.Sleep(1000);
            eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.InputEvent,
                    Message = "MOVE_RIGHT_STOP"
                });
                
            Assert.AreNotEqual(firstPos.X, player.GetPosition().X);
        }
}
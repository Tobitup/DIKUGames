using NUnit.Framework;
using System;
using System.IO;
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

namespace Tests;
[TestFixture]
public class TestPlayer {
    [SetUp]
    public void Init() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        //player = new Player(
        //new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
        //new Image(Path.Combine("Assets", "Images", "Player.png")));
        //Game game = new Game();
        //game.Player.Shape.Position;
        /*
        Here you should:
        (1) Initialize a GalagaBus with proper GameEventTypes 

        (2) Instantiate the StateMachine
        (3) Subscribe the GalagaBus to proper GameEventTypes and GameEventProcessors
        */
    }

}
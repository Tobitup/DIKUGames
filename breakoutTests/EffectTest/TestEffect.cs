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
using Breakout.BallClass;

namespace breakoutTests.EffectTest;

[TestFixture]
    public class TestEffects {
        

        [SetUp]
        public void Init() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            
        }



}
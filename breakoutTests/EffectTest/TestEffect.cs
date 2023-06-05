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
using Breakout.Effect;

namespace breakoutTests.EffectTest;

[TestFixture]
    public class TestEffects {
            Vec2F Effectsize;
            Vec2F Effectpos;
            DynamicShape EffectShape;
            IBaseImage Effectimage;

            Vec2F PlayerSize;
            Vec2F PlayerPos;
            DynamicShape PlayerShape;
            IBaseImage PlayerImage;
            GameEventBus eventBus = Breakout.BreakoutBus.GetBus();


        [SetUp]
        public void Init() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();

            Effectsize = new Vec2F(0.0f, 0.0f);
            Effectpos = new Vec2F(0.0f, 0.0f);
            EffectShape = new DynamicShape(Effectsize, Effectpos);
            Effectimage = new Image(Path.Combine(LevelLoader.MAIN_PATH,"Assets", "Images",
                                    EffectTransformer.TransformEffectToPath(Effects.BigJim)));

            PlayerPos = new Vec2F(0.4f, 0.1f);
            PlayerSize = new Vec2F(0.22f, 0.025f);
            PlayerShape = new DynamicShape(PlayerPos, PlayerSize);
            PlayerImage = new Image(Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Images",
                                                                                    "player.png"));
        }

        [Test]
        [Repeat(30)]
        public void EffectFactoryReturnsIEffect() {
            var newEffect = EffectFactory.GetRandomEffect(new Vec2F(0.5f, 0.5f));

            Assert.IsInstanceOf<IEffect>(newEffect);
        }


}
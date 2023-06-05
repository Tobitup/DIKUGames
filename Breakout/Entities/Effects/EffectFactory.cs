using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.Levels;

namespace Breakout.Effect;
public static class EffectFactory {
    public static Random rnd = new Random();
    private const float WIDTH = 0.05f;
    private const float HEIGTH = 0.05f;

    ///<summary>
    /// Returns a randomly selected power-up effect at the specified position.
    ///</summary>
    ///<param name="pos">The position where the power-up effect will be spawned.</param>
    ///<returns>The randomly selected power-up effect.</returns>
    public static IEffect GetRandomEffect(Vec2F pos) {
        int randomNumber = rnd.Next(1,9);
        switch (randomNumber) {
            case 1:
                return new BigJimPowerUp
                                    (new DynamicShape(pos, new Vec2F(WIDTH, HEIGTH)), 
                                    new Image(Path.Combine(LevelLoader.MAIN_PATH,"Assets", "Images",
                                    EffectTransformer.TransformEffectToPath(Effects.BigJim))));
            case 2:
                return new SlimJimHazard
                                    (new DynamicShape(pos, new Vec2F(WIDTH, HEIGTH)), 
                                    new Image(Path.Combine(LevelLoader.MAIN_PATH,"Assets", "Images",
                                    EffectTransformer.TransformEffectToPath(Effects.SlimJim))));
            case 3:
                return new LifeUP
                                    (new DynamicShape(pos, new Vec2F(WIDTH, HEIGTH)), 
                                    new Image(Path.Combine(LevelLoader.MAIN_PATH,"Assets", "Images",
                                    EffectTransformer.TransformEffectToPath(Effects.LifeUp))));
            case 4:
                return new LifeDown
                                    (new DynamicShape(pos, new Vec2F(WIDTH, HEIGTH)), 
                                    new Image(Path.Combine(LevelLoader.MAIN_PATH,"Assets", "Images",
                                    EffectTransformer.TransformEffectToPath(Effects.LifeDown))));
            case 5:
                return new SpeedyGonzales
                                    (new DynamicShape(pos, new Vec2F(WIDTH, HEIGTH)), 
                                    new Image(Path.Combine(LevelLoader.MAIN_PATH,"Assets", "Images",
                                    EffectTransformer.TransformEffectToPath(Effects.SpeedyGonzales))));
            case 6:
                return new BigBalls
                                    (new DynamicShape(pos, new Vec2F(WIDTH, HEIGTH)), 
                                    new Image(Path.Combine(LevelLoader.MAIN_PATH,"Assets", "Images",
                                    EffectTransformer.TransformEffectToPath(Effects.BigBalls))));
            default:
                return new SlimJimHazard
                                    (new DynamicShape(pos, new Vec2F(WIDTH, HEIGTH)), 
                                    new Image(Path.Combine(LevelLoader.MAIN_PATH,"Assets", "Images",
                                    EffectTransformer.TransformEffectToPath(Effects.SlimJim))));
        }
    }
}
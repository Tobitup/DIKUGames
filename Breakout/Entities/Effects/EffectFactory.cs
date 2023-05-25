using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace Breakout.Effect;
public static class EffectFactory {
    public static Random rnd = new Random();
    private const float WIDTH = 0.05f;
    private const float HEIGTH = 0.05f;


    public static IEffect GetRandomPowerUp(Vec2F pos) {
        int randomNumber = rnd.Next(4,5);
        //int randomNumber = 1;
        switch (randomNumber) {
            case 1:
                return new BigJimPowerUp
                                    (new DynamicShape(pos, new Vec2F(WIDTH, HEIGTH)), 
                                    new Image(Path.Combine("Assets", "Images",
                                    EffectTransformer.TransformEffectToPath(Effects.BigJim))));
            case 2:
                return new SlimJimHazard
                                    (new DynamicShape(pos, new Vec2F(WIDTH, HEIGTH)), 
                                    new Image(Path.Combine("Assets", "Images",
                                    EffectTransformer.TransformEffectToPath(Effects.SlimJim))));
            case 3:
                return new SplitzyPowerUp
                                    (new DynamicShape(pos, new Vec2F(WIDTH, HEIGTH)), 
                                    new Image(Path.Combine("Assets", "Images",
                                    EffectTransformer.TransformEffectToPath(Effects.Splitzy))));
            case 4:
                return new LifeUP
                                    (new DynamicShape(pos, new Vec2F(WIDTH, HEIGTH)), 
                                    new Image(Path.Combine("Assets", "Images",
                                    EffectTransformer.TransformEffectToPath(Effects.LifeUp))));
            case 5:
                return new LifeDown
                                    (new DynamicShape(pos, new Vec2F(WIDTH, HEIGTH)), 
                                    new Image(Path.Combine("Assets", "Images",
                                    EffectTransformer.TransformEffectToPath(Effects.LifeDown))));
            default:
                return new SlimJimHazard
                                    (new DynamicShape(pos, new Vec2F(WIDTH, HEIGTH)), 
                                    new Image(Path.Combine("Assets", "Images",
                                    EffectTransformer.TransformEffectToPath(Effects.SlimJim))));
        }
    }
}
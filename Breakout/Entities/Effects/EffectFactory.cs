using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace Breakout.Effects;
public static class EffectFactory {
    public static Random rnd = new Random();
    public static IEffect GetRandomPowerUp() {
        int randomNumber = rnd.Next(2);
        switch (randomNumber) {
            case 1:
                return new BigJimPowerUp(new StationaryShape(new Vec2F(0.5f,0.0f), new Vec2F(0.5f,0.0f)), new Image(Path.Combine("Assets", "Images",
                                                                                    "player.png")));
            default:
                return new BigJimPowerUp(new StationaryShape(new Vec2F(0.5f,0.0f), new Vec2F(0.5f,0.0f)), new Image(Path.Combine("Assets", "Images",
                                                                                    "player.png")));
        }
    }
}
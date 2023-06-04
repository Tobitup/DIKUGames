using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
namespace Breakout.Blocks;

public static class BlockFactory
{
    public static Entity CreateNewBlock(string type, Vec2F position, Vec2F size, IBaseImage image)
    {
        switch (type)
        {
            case "Unbreakable":
                return new UnbreakableBlock(position, size, image);
            case "Moving":
                return new MovingBlock(position, size, image);
            case "Normal":
                return new NormalBlock(position, size, image);
            case "PowerUp":
                return new PowerUpBlock(position, size, image);
            default:
                return new NormalBlock(position, size, image);
        }
    }
}
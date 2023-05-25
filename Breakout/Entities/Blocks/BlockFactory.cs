using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
namespace Breakout.Blocks;

public static class BlockFactory
{
    public static Entity CreateNewBlock(string type, Vec2I positionInArray, IBaseImage image)
    {
        switch (type)
        {
            case "Unbreakable":
                return new UnbreakableBlock(positionInArray, image);
            case "Moving":
                return new MovingBlock(positionInArray, image);
            case "Normal":
                return new NormalBlock(positionInArray, image);
            case "PowerUp":
                return new PowerUpBlock(positionInArray, image);
            default:
                return new NormalBlock(positionInArray, image);
        }
    }
}
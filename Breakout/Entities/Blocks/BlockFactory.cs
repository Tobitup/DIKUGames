using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
namespace Breakout.Blocks;

public static class BlockFactory
{
    /// <summary>
    /// Simple factory which creates a new block entity of the specified type with the given 
    /// position, size, and image.
    /// </summary>
    /// <param name="type"> The type of the block to create. </param>
    /// <param name="position"> The position of the block. </param>
    /// <param name="size"> The size of the block. </param>
    /// <param name="image"> The image of the block. </param>
    /// <returns> The newly created block entity. </returns>
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
                return new EffectBlock(position,size, image);
            default:
                return new NormalBlock(position, size, image);
        }
    }
}
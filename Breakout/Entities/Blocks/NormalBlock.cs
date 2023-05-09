using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Blocks;

public class NormalBlock : Block
{

    private int hitpoints;

    private int value;

    /// <summary> Initializes a new instance of the Block class with the specified position 
    ///           and image. </summary>
    /// <param name="positionInArray"> The position of the block in the array. </param>
    /// <param name="image"> The image to be used for the block. </param>
    /// <return> Returns a Block with a given size, position and image. </return>
    public NormalBlock(Vec2I positionInArray, IBaseImage image) : base((positionInArray), image)
    {
        // Temporary HitPoints for Normal Block.
        hitpoints = 1;
        // Temporary Value for Normal Block.
        value = 1;
    }

}
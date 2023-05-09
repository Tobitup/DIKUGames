using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Blocks;

public class Unbreakable : Block
{

    private int hitpoints;

    private int value;

    /// <summary> Initializes a new instance of the Block class with the specified position 
    ///           and image. </summary>
    /// <param name="positionInArray"> The position of the block in the array. </param>
    /// <param name="image"> The image to be used for the block. </param>
    /// <return> Returns a Block with a given size, position and image. </return>
    public Unbreakable(Vec2I positionInArray, IBaseImage image) : base((positionInArray), image)
    {
        //placeholder hp
        hitpoints = 1;
        //placeholder value
        value = 1;
    }

    /// <summary> Makes sure unbreakable blocks do not break </summary>
    /// <return> Void. </return>
    public override void TakeDamage()
    {
        //do nothing
    }
}
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Blocks;

public class Moving : Block
{

    private int hitpoints;

    private int value;

    bool movingRight = false;

    private float movementSpeed = 0.004f;
    public float MovementSpeed
    {
        get { return movementSpeed; }
    }
    /// <summary> Initializes a new instance of the Block class with the specified position 
    ///           and image. </summary>
    /// <param name="positionInArray"> The position of the block in the array. </param>
    /// <param name="image"> The image to be used for the block. </param>
    /// <return> Returns a Block with a given size, position and image. </return>
    public Moving(Vec2I positionInArray, IBaseImage image) : base((positionInArray), image)
    {
        //placeholder hp
        hitpoints = 1;
        //placeholder value
        value = 1;
    }

    public override void Update()
    {
        MoveMoving();
    }
    public void MoveMoving()
    {
        if (this.Shape.Position.X >= 1.0f - Shape.Extent.X)
        {
            movingRight = false;
        }
        else if (this.Shape.Position.X <= 0.0f)
        {
            movingRight = true;
        }
        if (movingRight)
        {
            float currentPosition = this.Shape.Position.X;
            float newPosition = currentPosition + movementSpeed;
            this.Shape.Position.X = newPosition;

        }
        else //moving left
        {
            float currentPosition = this.Shape.Position.X;
            float newPosition = currentPosition - movementSpeed;
            this.Shape.Position.X = newPosition;
        }
    }
}
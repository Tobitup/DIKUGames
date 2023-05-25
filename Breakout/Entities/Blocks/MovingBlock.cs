using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Blocks;

public class MovingBlock : Entity, IBlock
{

    private int hitpoints;
    public int HitPoints {get {return hitpoints;}}

    public uint Value { get { return (uint)value; } }
    private int value;

    bool movingRight = false;

    private float movementSpeed = 0.004f;

    private const float HEIGHT = WIDTH/2f;
    private const float WIDTH = 1f/12;

    private const float OFFSET = 3 * HEIGHT;
    public float MovementSpeed
    {
        get { return movementSpeed; }
    }

    public Shape GetShape {get {return base.Shape;}}

    /// <summary> Initializes a new instance of the Block class with the specified position 
    ///           and image. </summary>
    /// <param name="positionInArray"> The position of the block in the array. </param>
    /// <param name="image"> The image to be used for the block. </param>
    /// <return> Returns a Block with a given size, position and image. </return>
    public MovingBlock(Vec2I positionInArray, IBaseImage image) : base(new DynamicShape(
                new Vec2F(positionInArray.X * WIDTH, positionInArray.Y * HEIGHT-OFFSET),
                                                            new Vec2F(WIDTH, HEIGHT)), image) {
        hitpoints = 1;
        value = 1;
    }

  
    public void MoveMoving() {
        if (this.Shape.Position.X >= 1.0f - Shape.Extent.X) {
            movingRight = false;
        } else if (this.Shape.Position.X <= 0.0f) {
            movingRight = true;
        }
        if (movingRight) {
            float currentPosition = this.Shape.Position.X;
            float newPosition = currentPosition + movementSpeed;
            this.Shape.Position.X = newPosition;

        } else /*moving left*/ {
            float currentPosition = this.Shape.Position.X;
            float newPosition = currentPosition - movementSpeed;
            this.Shape.Position.X = newPosition;
        }
    }

    /// <summary> Reduces the hitpoints of a block by 1. </summary>
    /// <return> Void. </return>
    public void TakeDamage() {
        hitpoints--;
        RemoveIfDead();
    }
    public virtual void Update() {
        MoveMoving();
    }

    /// <summary> Checks if a block has 0 or less Hitpoints, in which case it is dead. </summary>
    /// <return> Boolean value to indicate if the block is dead or not. </return>
    public bool IsDead() {
        if (hitpoints <= 0) {
            return true;
        }
        return false;
    }

    /// <summary> Checks if a block IsDead and if true deletes the entity. </summary>
    /// <return> Void. </return>
    public void RemoveIfDead() {
        if (IsDead()) {
            DeleteEntity();
        }
    }
}

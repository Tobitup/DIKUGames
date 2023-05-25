using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Blocks;

public class UnbreakableBlock : Entity, IBlock {

    private int hitpoints;
    public int HitPoints {get {return hitpoints;}}

    private int value;

    public uint Value { get { return (uint)value; } }

    private const float HEIGHT = WIDTH/2f;
    private const float WIDTH = 1f/12;

    private const float OFFSET = 3 * HEIGHT;

    /// <summary> Initializes a new instance of the Block class with the specified position 
    ///           and image. </summary>
    /// <param name="positionInArray"> The position of the block in the array. </param>
    /// <param name="image"> The image to be used for the block. </param>
    /// <return> Returns a Block with a given size, position and image. </return>
    public UnbreakableBlock(Vec2I positionInArray, IBaseImage image) : base(new DynamicShape(
                new Vec2F(positionInArray.X * WIDTH, positionInArray.Y * HEIGHT-OFFSET),
                                                            new Vec2F(WIDTH, HEIGHT)), image) {
        hitpoints = 1;
        value = 1;
    }

    /// <summary> Makes sure unbreakable blocks do not break </summary>
    /// <return> Void. </return>
    public void TakeDamage() {
        //do nothing
    }

    public virtual void Update() {
        //do nothing
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
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using Breakout.Effects;

namespace Breakout.Blocks;

public class PowerUpBlock : Entity, IBlock, ISpecialBlock {

    private int hitpoints;

    private int value;

    private const float HEIGHT = 0.041f;
    private const float WIDTH = 0.0833f;
    private IEffect effect;
    public uint Value { get { return (uint)value; } }

    public int HitPoints {get {return hitpoints;}}

    /// <summary> Initializes a new instance of the Block class with the specified position 
    ///           and image. </summary>
    /// <param name="positionInArray"> The position of the block in the array. </param>
    /// <param name="image"> The image to be used for the block. </param>
    /// <return> Returns a Block with a given size, position and image. </return>
    public PowerUpBlock(Vec2I positionInArray, IBaseImage image) : base(new DynamicShape(
                new Vec2F(positionInArray.X * WIDTH, positionInArray.Y * HEIGHT-HEIGHT),
                                                            new Vec2F(WIDTH, HEIGHT)), image) {
        // Temporary HitPoints for Normal Block.
        hitpoints = 1;
        // Temporary Value for Normal Block.
        value = 1;

        effect = EffectFactory.GetRandomPowerUp();
    }
    /// <summary> Reduces the hitpoints of a block by 1. </summary>
    /// <return> Void. </return>
    public void TakeDamage() {
        hitpoints--;
        RemoveIfDead();
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

    public IEffect GetEffect() {
        throw new NotImplementedException();
    }


}
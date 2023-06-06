using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using Breakout.Effect;

namespace Breakout.Blocks;

public class EffectBlock : Entity, IBlock, ISpecialBlock {

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
    public EffectBlock(Vec2F position, Vec2F size, IBaseImage image) : 
    base(new DynamicShape(position, size), image) {
        hitpoints = 1;
        value = 1;
        effect = EffectFactory.GetRandomEffect(base.Shape.Position);
    }

    /// <summary> Reduces the hitpoints of a block by 1. </summary>
    public void TakeDamage() {
        hitpoints--;
        RemoveIfDead();
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
    public void RemoveIfDead() {
        if (IsDead()) {
            DeleteEntity();
        }
    }

    /// <summary> Retrieves the entity associated with the effect. </summary>
    public Entity GetEffect() {
        return effect.GetEntity;
    }

    public virtual void Update() {
        //Do nothing
    }

}
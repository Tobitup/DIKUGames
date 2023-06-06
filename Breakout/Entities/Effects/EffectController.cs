using DIKUArcade.Entities;
using DIKUArcade.Physics;
using Breakout.Blocks;
using Breakout.BallClass;

namespace Breakout.Effect;

public static class EffectController {

    /// <summary> Updates the effects in an entitycontainer. </summary> 
    /// <param name="effectsContainer"> The entitycontainer to update. </param>
    public static void UpdateEffects(EntityContainer<Entity> effectsContainer) {
        foreach (IEffect effect in effectsContainer) {
            effect.Update();
        }
    }

    ///<summary> Checks if special blocks are dead and if so, spawns an effect. </summary>
    ///<param name="blockContainer"> The container of blocks to check for special blocks. </param>
    ///<param name="effectsContainer"> The container to add the spawned effects to. </param>
    public static void SpawnEffect(EntityContainer<Entity> blockContainer,
                                                        EntityContainer<Entity> effectsContainer) {
        foreach (IBlock block in blockContainer) {
            var specialBlock = block as ISpecialBlock;
            if (specialBlock != null && specialBlock.IsDead()) {
                effectsContainer.AddEntity(specialBlock.GetEffect());
            }
        }
    }

    /// <summary>
    /// Handles collision between entities in the effects container and the player.
    /// </summary>
    ///<param name="effectsContainer">The container of effects to check for collisions.</param>
    ///<param name="player">The player entity.</param>
    public static void CollisionEffect(EntityContainer<Entity> effectsContainer, 
                                                                    Player.Player player) {
        effectsContainer.Iterate(effect => {
            var effects = effect.Shape.AsDynamicShape();
            CollisionData collision = CollisionDetection.Aabb(effects, player.Shape);
            if (collision.Collision) {
                var collidedEffect = effect as IEffect;
                collidedEffect.InitiateEffect();
                effect.DeleteEntity();           
            }
            if (effect.Shape.Position.Y < 0.0f) {
                effect.DeleteEntity();
            }
        });
    }
}
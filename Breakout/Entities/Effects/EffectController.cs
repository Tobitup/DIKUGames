using DIKUArcade.Entities;
using DIKUArcade.Physics;
using Breakout.Blocks;
using Breakout.BallClass;

namespace Breakout.Effect;

public static class EffectController {

    public static void UpdateEffects(EntityContainer<Entity> effectsContainer) {
        foreach (IEffect effect in effectsContainer) {
            effect.Update();
        }
    }

    public static void SpawnEffect(EntityContainer<Entity> blockContainer,
                                                        EntityContainer<Entity> effectsContainer) {
        foreach (IBlock block in blockContainer) {
            var specialBlock = block as ISpecialBlock;
            if (specialBlock != null && specialBlock.IsDead()) {
                effectsContainer.AddEntity(specialBlock.GetEffect());
            }
        }
    }

    public static void CollisionEffect(EntityContainer<Entity> effectsContainer, Player.Player player) {
        effectsContainer.Iterate(effect => {
            var effects = effect.Shape.AsDynamicShape();
            CollisionData collision = CollisionDetection.Aabb(effects, player.Shape);
            if (collision.Collision) {
                var collidedEffect = effect as IEffect;
                collidedEffect.InitiateEffect();
                System.Console.WriteLine("Collided");
                effect.DeleteEntity();           
            }
            if (effect.Shape.Position.Y < 0.0f) {
                effect.DeleteEntity();
            }
        });
    }
    public static void InitiateEffect(string effect) {
        switch (EffectTransformer.TransformStringToEffect(effect)) {
            case Effects.Splitzy:
                //SplitBalls();
                System.Console.WriteLine("Splitzy");
                break;
            case Effects.SlimJim:
                System.Console.WriteLine("SlimJim");
            break;
            default:
                break;
        }
    }

    private static void SplitBalls(EntityContainer<Ball> ballContainer) {
        EntityContainer<Ball> tempBallContainer = new EntityContainer<Ball>();
        foreach(Ball ball in ballContainer) {
            tempBallContainer.AddEntity(ball);
            for (int i=0; i<3; i++) {
                tempBallContainer.AddEntity(BallFactory.GenerateRandomDirBall(ball.Shape.Position));
            }
        }
        ballContainer = tempBallContainer;
    }

}
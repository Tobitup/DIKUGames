using DIKUArcade.Entities;
using DIKUArcade.Physics;
using Breakout.Blocks;
using Breakout.PlayerLives;

namespace Breakout.BallClass;

public static class CollisionController {
    public static void IterateCollision(EntityContainer<Ball> ballContainer, Player.Player player, 
                                        EntityContainer<Entity> blockContainer) {
        ballContainer.Iterate(ball => {
            var activeBall = ball.Shape.AsDynamicShape();
            var activePlayer = player.Shape;
            var ballPlayerDetect = CollisionDetection.Aabb(activeBall, activePlayer);
            if (ballPlayerDetect.Collision) {
                    BallMath.DirUp(ball,activePlayer.Position , activePlayer.Extent);
            } else {
                foreach (IBlock block in blockContainer) {
                    // Deletes ball if it leaves the window.
                    var ballBlockDetect = CollisionDetection.Aabb(activeBall, block.Shape);
                    if (activeBall.Position.Y <= 0.01f || 
                        activeBall.Position.Y + activeBall.Extent.Y <= 0.01f) {
                            ball.DeleteEntity();

                    } else if (ballBlockDetect.Collision) {
                        if (ballBlockDetect.CollisionDir == CollisionDirection.CollisionDirRight || 
                            ballBlockDetect.CollisionDir == CollisionDirection.CollisionDirLeft) {
                                BallMath.DirLR(ball);
                                block.TakeDamage();
                        }
                        if (ballBlockDetect.CollisionDir == CollisionDirection.CollisionDirUp || 
                            ballBlockDetect.CollisionDir == CollisionDirection.CollisionDirDown) {
                                BallMath.DirUD(ball);
                                block.TakeDamage();
                        }
                    }}
            ball.Move(ball);   
            }
        });
    }
}
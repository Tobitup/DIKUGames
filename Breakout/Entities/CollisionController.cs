using DIKUArcade.Entities;
using DIKUArcade.Physics;
using Breakout.Blocks;
using Breakout.PlayerLives;

namespace Breakout.BallClass;

public static class CollisionController {

    public static void Move(Ball activeBall) {
        // if (activeBall.Shape.Position.X > 0.0f && activeBall.Shape.Position.X + 
        //                     activeBall.Shape.Extent.X< 1.0f
        //                     && activeBall.Shape.Position.Y > 0.0f && activeBall.Shape.Position.Y + 
        //                     activeBall.Shape.Extent.Y< 1.0f) {
        //     activeBall.Shape.Move();
        //     }
        if (activeBall.Shape.Position.X <= 0.01f || activeBall.Shape.Position.X + 
            activeBall.Shape.Extent.X <= 0.01f || activeBall.Shape.Position.X >= 0.99f 
            || activeBall.Shape.Position.X + activeBall.Shape.Extent.X >= 0.99f) {
            BallMath.DirLR(activeBall);
        }
        if (activeBall.Shape.Position.Y >= 0.99f || 
                                activeBall.Shape.Position.Y + activeBall.Shape.Extent.Y >= 0.99f) {
            BallMath.DirUD(activeBall);
        }
    }

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
            Move(ball);   
            }
        });
    }
}
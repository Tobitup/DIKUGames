using DIKUArcade.Entities;
using DIKUArcade.Physics;
using Breakout.Blocks;
using Breakout.PlayerLives;

namespace Breakout.BallClass;

public static class CollisionController {

    /// <summary>
    /// Moves the input ball according to its current direction and handles boundary collisions.
    /// </summary>
    /// <param name="activeBall"> The ball to move. </param>
    public static void Move(Ball activeBall) {
        if (activeBall.Shape.Position.X <= 0.01f || activeBall.Shape.Position.X + 
            activeBall.Shape.Extent.X <= 0.01f || activeBall.Shape.Position.X >= 0.99f 
            || activeBall.Shape.Position.X + activeBall.Shape.Extent.X >= 0.99f) {
            BallMath.DirLR(activeBall);
        }
        if (activeBall.Shape.Position.Y >= 0.99f || 
                                activeBall.Shape.Position.Y + activeBall.Shape.Extent.Y >= 0.99f) {
            BallMath.DirUD(activeBall);
        }
        activeBall.Shape.Move();
    }

    /// <summary>
    /// Iterates through the balls in the ballContainer and performs collision detection with 
    /// the player and blocks. Along with handeling collisions by changing the ball's direction 
    /// vector and updating block states.
    /// </summary>
    /// <param name="ballContainer"> The entity container of balls. </param>
    /// <param name="player"> The player object. </param>
    /// <param name="blockContainer"> The enetity container of blocks. </param>
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
                    var ballBlockDetect = CollisionDetection.Aabb(activeBall, block.Shape);
                    // Deletes ball if it leaves the window.
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
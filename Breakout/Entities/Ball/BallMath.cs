using DIKUArcade.Math;

namespace Breakout.BallClass;

public static class BallMath {

    /// <summary>
    /// Changes the direction of the single ball to move upwards based on where 
    /// it makes contant with the player.
    /// </summary>
    /// <param name="singleBall"> The ball to modify the direction of. </param>
    /// <param name="playerPos"> The position of the player. </param>
    /// <param name="playerExtend"> The extent of the player. </param>
    public static void DirUp(Ball singleBall, Vec2F playerPos , Vec2F playerExtend) {
        var activeBall = singleBall.Shape.AsDynamicShape();
        // Calculate the players middle X possition.
        var playerMidX = playerPos + playerExtend / 2;

        // Calculates the ball's middle X possition.
        var ballMidX = activeBall.Position + activeBall.Extent / 2;
        
        // Calculates the difference between the players middle and balls middle, this being the 
        // offset used later to calculate the new vector for the balls dirrection.
        var ballPlayerDif = ballMidX - playerMidX;

        // Gets the balls speed so that the magnitude will always stay constant.
        var ballSpeed = activeBall.Direction.Length();

        // Normalizes the vector and clamps its X component so if the ball collides with the player
        // on the very left or right side, the ball won't shoot off in extreme angles.
        var normalizedPos = Math.Clamp(Vec2F.Normalize(ballPlayerDif).X, -0.8f,0.8f);

        // "Draws" a half circle around the player to calculate the dirrection of the new Y vector,
        // Then multiplies it with the vectors magnitude to maintain a constant speed. 
        var newDir = new Vec2F(normalizedPos, MathF.Sqrt(1.0f - normalizedPos * normalizedPos)) * 
                                                                                (float)ballSpeed;
        ChangeDirection(singleBall, newDir);
    }

    /// <summary>
    /// Modifies the direction of the input ball to move in the opposite horizontal direction.
    /// </summary>
    /// <param name="singleBall"> The ball to modify the direction of. </param>
    public static void DirLR(Ball singleBall) {
        var activeBall = singleBall.Shape.AsDynamicShape();
        var newDirection = activeBall.Direction = new Vec2F(
                                activeBall.Direction.X*(-1),
                                activeBall.Direction.Y);
        ChangeDirection(singleBall,newDirection);
    }

    /// <summary>
    /// Modifies the direction of the input ball to move in the opposite vertical direction.
    /// </summary>
    /// <param name="singleBall"> The ball to modify the direction of. </param>
    public static void DirUD(Ball singleBall) {
        var activeBall = singleBall.Shape.AsDynamicShape();
        var newDirection = activeBall.Direction = new Vec2F(
                            activeBall.Direction.X,
                            activeBall.Direction.Y*(-1));
        ChangeDirection(singleBall,newDirection);
    }
    
    /// <summary>
    /// Changes the direction of the input ball to the given direction vector.
    /// </summary>
    /// <param name="singleBall"> The ball to modify the direction of. </param>
    /// <param name="newDir"> The balls new direction vector. </param>
    public static void ChangeDirection(Ball singleBall, Vec2F newDir) {
        singleBall.Shape.AsDynamicShape().ChangeDirection(newDir);
    }
}
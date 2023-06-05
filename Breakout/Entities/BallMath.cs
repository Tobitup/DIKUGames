using DIKUArcade.Math;

namespace Breakout.BallClass;

public static class BallMath {

    // Called from CollisionController to handle Ball's new dirrection when colliding with Player.
    public static void DirUp(Ball singleBall, Vec2F playerPos , Vec2F playerExtend) {
        var activeBall = singleBall.Shape.AsDynamicShape();
        // Calculate players middle X possition.
        var playerMidX = playerPos + playerExtend / 2;

        // Calculates ball middle X possition.
        var ballMidX = activeBall.Position + activeBall.Extent / 2;
        
        // Calculates the different between the player middle and ball middle. 
        // Used to calculate the new vector for the balls dirrection.
        var ballPlayerDif = ballMidX - playerMidX;

        // Gets the balls speed so that it's magnitude will always stay constant.
        var ballSpeed = activeBall.Direction.Length();

        // Normalizes the vector and clamps it so if the ball collides with the player on the very 
        // left or right side, the ball won't shoot off almost sideways.
        var normalizedPos = Math.Clamp(Vec2F.Normalize(ballPlayerDif).X, -0.8f,0.8f);

        // "Draws" a half circle around the player to calculate the dirrection of the new Y vector,
        // Then times it with the vectors magnitude to have a constant speed. 
        var newDir = new Vec2F(normalizedPos, MathF.Sqrt(1.0f - normalizedPos * normalizedPos)) * 
                                                                                (float)ballSpeed;
        ChangeDirection(singleBall, newDir);
    }

    // Called from CollisionController and Ball to update ball direction when collision is 
    //                                                                  detected Left and Right.
    public static void DirLR(Ball singleBall) {
        var activeBall = singleBall.Shape.AsDynamicShape();
        var newDirection = activeBall.Direction = new Vec2F(
                                activeBall.Direction.X*(-1),
                                activeBall.Direction.Y);
        ChangeDirection(singleBall,newDirection);
    }

    // Called from CollisionController and Ball to update ball direction when collision is 
    //                                                                  detected Up and Down.
    public static void DirUD(Ball singleBall) {
        var activeBall = singleBall.Shape.AsDynamicShape();
        var newDirection = activeBall.Direction = new Vec2F(
                            activeBall.Direction.X,
                            activeBall.Direction.Y*(-1));
        ChangeDirection(singleBall,newDirection);
    }
    
    public static void ChangeDirection(Ball singleBall, Vec2F newDir) {
        singleBall.Shape.AsDynamicShape().ChangeDirection(newDir);
    }
}
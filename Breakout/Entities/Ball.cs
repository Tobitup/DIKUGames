using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;

namespace Breakout.BallClass;

public class Ball : Entity {


    public Ball(DynamicShape shape, IBaseImage image) : base (shape, image) { }
    public void Move() {
        if (Shape.Position.X > 0.0f && Shape.Position.X + Shape.Extent.X< 1.0f
                && Shape.Position.Y > 0.0f && Shape.Position.Y + Shape.Extent.Y< 1.0f ) {
            base.Shape.Move();
            }
        if (Shape.Position.X <= 0.01f || Shape.Position.X + Shape.Extent.X <= 0.01f || 
                Shape.Position.X >= 0.99f || Shape.Position.X + Shape.Extent.X >= 0.99f) {
            DirLR();
        }
        if (Shape.Position.Y >= 0.99f || Shape.Position.Y + Shape.Extent.Y >= 0.99f) {
            DirUD();
        }
    }


    // Called from GameRunning to handle Ball's new dirrection when colliding with Player.
    public void DirUp(float playerPos , float playerExtend) {
        var activeBall = this.Shape.AsDynamicShape();
        var ballAngle = ((activeBall.Position.X - playerPos) / playerPos);
        var newDirection = activeBall.Direction = new Vec2F(
                (float)((activeBall.Direction.X * Math.Cos(ballAngle) + 
                        activeBall.Direction.Y * Math.Sin(ballAngle))),
                        activeBall.Direction.Y*-1);
        newDirection *= (float)(activeBall.Direction.Length() / newDirection.Length());
        ChangeDirection(newDirection);

    }

    // Called from GameRunning to update ball direction when collision is detected Left and Right.
    public void DirLR() {
        var activeBall = this.Shape.AsDynamicShape();
        var newDirection = activeBall.Direction = new Vec2F(
                                activeBall.Direction.X*(-1),
                                activeBall.Direction.Y);
        ChangeDirection(newDirection);
    }

    // Called from GameRunning to update ball direction when collision is detected Up and Down.
    public void DirUD() {
        var activeBall = this.Shape.AsDynamicShape();
        var newDirection = activeBall.Direction = new Vec2F(
                            activeBall.Direction.X,
                            activeBall.Direction.Y*(-1));
        ChangeDirection(newDirection);
    }
    
    public void ChangeDirection(Vec2F newDir) {
        base.Shape.AsDynamicShape().ChangeDirection(newDir);
    }
}
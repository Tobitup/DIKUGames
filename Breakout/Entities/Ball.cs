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


    public Ball(DynamicShape shape, IBaseImage image) : base (shape, image) {
    }

    private void DirChangeUpDown() {
        var newDirection = base.Shape.AsDynamicShape().Direction = new Vec2F(
                                                    base.Shape.AsDynamicShape().Direction.X,
                                                    base.Shape.AsDynamicShape().Direction.Y*(-1));
        ChangeDirection(newDirection);
    }

    private void DirChangeLeftRight() {
        var newDirection = base.Shape.AsDynamicShape().Direction = new Vec2F(
                                                    base.Shape.AsDynamicShape().Direction.X*(-1),
                                                    base.Shape.AsDynamicShape().Direction.Y);
        ChangeDirection(newDirection);
    }

    public void Move() {
        if (Shape.Position.X > 0.0f && Shape.Position.X + Shape.Extent.X< 1.0f
                && Shape.Position.Y > 0.0f && Shape.Position.Y + Shape.Extent.Y< 1.0f ) {
            base.Shape.Move();
            }
        if (Shape.Position.X <= 0.01f || Shape.Position.X + Shape.Extent.X <= 0.01f || 
                Shape.Position.X >= 0.99f || Shape.Position.X + Shape.Extent.X >= 0.99f) {
            DirChangeLeftRight();
        }
        if (Shape.Position.Y >= 0.99f || Shape.Position.Y + Shape.Extent.Y >= 0.99f) {
            DirChangeUpDown();
        }
        if (Shape.Position.Y <= 0.01f || Shape.Position.Y + Shape.Extent.Y <= 0.01f) {
            
        }
    }
    
    public void ChangeDirection(Vec2F newDir) {
        base.Shape.AsDynamicShape().ChangeDirection(newDir);
    }
}
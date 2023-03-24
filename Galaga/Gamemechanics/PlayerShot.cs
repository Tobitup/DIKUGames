namespace Galaga;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class PlayerShot : Entity {
    private static Vec2F extent = new Vec2F(0.008f,0.021f);
    private static Vec2F direction = new Vec2F(0.0f,0.1f);
    private static Vec2F Extent {
        get {return extent;}
        }
    public PlayerShot(Vec2F position, IBaseImage image) 
        : base(new DynamicShape(position, extent, direction), image) {
    }

    public void Move() {
        base.Shape.Position.Y += 0.1f;
    }
}
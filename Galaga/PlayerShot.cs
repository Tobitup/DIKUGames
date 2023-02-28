namespace Galaga;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;


public class PlayerShot : Entity {
    private static Vec2F extent = new Vec2F(0.008f,0.021f);
    private static Vec2F direction = new Vec2F(0.0f,0.0f);
    public static Vec2F Extent {
        get {return extent;}
        }
    public PlayerShot(Vec2F position, IBaseImage image) : base(new DynamicShape(position, new Vec2F(0.015f,0.08f)), image) {
    }

    public void Move() {
        base.Shape.Position.Y += 0.1f;
    }
}
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Effects;

public class BigJimPowerUp : Entity, IEffect {

    private const float MOVEMENT_SPEED = 0.005f;
    //Entity GetEntity {get {return this;}}

    Entity IEffect.GetEntity => this;

    public BigJimPowerUp(Shape shape, IBaseImage image) : base(shape, image)
    {
    }

    public void Update() {
        MoveEffect();
    }

    private void MoveEffect() {
        base.Shape.Position.Y -= MOVEMENT_SPEED;
    }

    
}
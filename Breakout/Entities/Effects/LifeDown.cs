using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Timers;

namespace Breakout.Effect;

public class LifeDown : Entity, IEffect {

    private const float MOVEMENT_SPEED = 0.005f;

    //Entity GetEntity {get {return this;}}

    Entity IEffect.GetEntity => this;

    public LifeDown(Shape shape, IBaseImage image) : base(shape, image){
        base.Shape.AsDynamicShape().ChangeDirection(new Vec2F(0.0f, -0.009f));
    }

    public void Update() {
        MoveEffect();
    }

    private void MoveEffect() {
        base.Shape.Move();
    }

    
    public Shape GetShape() {
        return base.Shape;
    }

    public void InitiateEffect() {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent
                {
                    EventType = GameEventType.PlayerEvent,
                    Message = "EFFECT",
                    StringArg1 = EffectTransformer.TransformEffectToString(Effects.LifeDown),
                    StringArg2 = "START",
                });

    }

}
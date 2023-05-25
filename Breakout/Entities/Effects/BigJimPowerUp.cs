using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Timers;

namespace Breakout.Effect;

public class BigJimPowerUp : Entity, IEffect {

    private const float MOVEMENT_SPEED = 0.005f;
    private TimePeriod EFFECT_DURATION = DIKUArcade.Timers.TimePeriod.NewSeconds(1.0);
    //Entity GetEntity {get {return this;}}

    Entity IEffect.GetEntity => this;

    public BigJimPowerUp(Shape shape, IBaseImage image) : base(shape, image){
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
                    StringArg1 = EffectTransformer.TransformEffectToString(Effects.BigJim),
                    StringArg2 = "START"
                });
        BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent
                {
                    EventType = GameEventType.PlayerEvent,
                    Message = "EFFECT",
                    StringArg1 = EffectTransformer.TransformEffectToString(Effects.BigJim),
                    StringArg2 = "STOP",
                    Id = (int) Effects.BigJim,
                }, EFFECT_DURATION);
    }

}
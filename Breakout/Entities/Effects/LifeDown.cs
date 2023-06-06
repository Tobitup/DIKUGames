using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout.Effect;

public class LifeDown : Entity, IEffect {

    private const float MOVEMENT_SPEED = 0.005f;
    Entity IEffect.GetEntity => this;

    ///<summary> Represents an effect that deducts a life. </summary>
    public LifeDown(Shape shape, IBaseImage image) : base(shape, image){
        base.Shape.AsDynamicShape().ChangeDirection(new Vec2F(0.0f, -0.009f));
    }
  
    /// <summary> Updates the effect. </summary>  
    public void Update() {
        MoveEffect();
    }
   
    /// <summary> Moves the effect by calling the Move method of the base Shape. </summary>
    private void MoveEffect() {
        base.Shape.Move();
    }

    /// <summary> Gets the shape of the effect entity. </summary>   
    public Shape GetShape() {
        return base.Shape;
    }
   
    /// <summary> Initiates the effect by registering the necessary game events. </summary>
    public void InitiateEffect() {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "EFFECT",
                    StringArg1 = EffectTransformer.TransformEffectToString(Effects.LifeDown),
                    StringArg2 = "START",
        });
    }
}
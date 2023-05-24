using DIKUArcade.Entities;

namespace Breakout.Effect;

public interface IEffect {

    Entity GetEntity {get;}

    Shape GetShape();

    void Update();

    void InitiateEffect();

}
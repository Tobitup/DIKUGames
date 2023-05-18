using DIKUArcade.Entities;

namespace Breakout.Effects;

public interface IEffect {

    Entity GetEntity {get;}
    void Update();

}
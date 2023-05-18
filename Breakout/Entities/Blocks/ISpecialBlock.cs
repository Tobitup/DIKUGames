using Breakout.Effects;
using DIKUArcade.Entities;

namespace Breakout.Blocks;

public interface ISpecialBlock {
    bool IsDead();
    Entity GetEffect();
}
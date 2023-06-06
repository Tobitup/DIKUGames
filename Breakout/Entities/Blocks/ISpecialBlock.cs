using DIKUArcade.Entities;

namespace Breakout.Blocks;
/// <summary> Interface for Blocks containing effects </summary>
public interface ISpecialBlock {
    bool IsDead();
    Entity GetEffect();
}
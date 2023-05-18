using Breakout.Effects;

namespace Breakout.Blocks;

public interface ISpecialBlock {
    IEffect GetEffect();
}
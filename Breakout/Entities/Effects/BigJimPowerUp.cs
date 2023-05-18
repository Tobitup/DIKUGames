using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Effects;

public class BigJimPowerUp : Entity, IEffect
{
    public BigJimPowerUp(Shape shape, IBaseImage image) : base(shape, image)
    {
    }
}
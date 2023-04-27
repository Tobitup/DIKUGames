using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Blocks;
public class Block : Entity { 
    public Block(Vec2I positionInArray, IBaseImage image) : base(new StationaryShape(
                                            new Vec2F(positionInArray.X * 0.083f - 0.04f, positionInArray.Y * 0.041f + 0.50f), 
                                new Vec2F(0.09f, 0.035f)), image) {
        
    }
}
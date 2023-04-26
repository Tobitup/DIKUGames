using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout.Blocks;
public class Block : Entity {
    StationaryShape TESTSHAPE = new StationaryShape(new Vec2F(0.0f,0.0f), new Vec2F(0.0f,0.0f));
    public Block(Vec2I positionInArray, IBaseImage image) : base(new StationaryShape(new Vec2F(0.0f,0.0f), new Vec2F(0.0f,0.0f)), image) {

    }
}
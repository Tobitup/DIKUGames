using DIKUArcade.Math;
using DIKUArcade.Graphics;
namespace Breakout.Blocks;

public static class BlockFactory
{
    public static Block CreateNewBlock(string type, Vec2I positionInArray, IBaseImage image)
    {
        switch (type)
        {
            case "Unbreakable":
                //System.Console.WriteLine("unbreakable block created");
                return new Unbreakable(positionInArray, image);
            case "Moving":
                //System.Console.WriteLine("moving block created");
                return new Moving(positionInArray, image);
            case "Normal":
                //System.Console.WriteLine("normal block created");
                return new NormalBlock(positionInArray, image);
            default:
                return new NormalBlock(positionInArray, image);
        }
    }
}
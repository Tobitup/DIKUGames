namespace Breakout.Levels;
public static class LevelTransformer
{
    /// <summary> Transforms a SelectLevel enum value to its corresponding file name string.
    /// </summary>
    /// <param name="level"> The SelectLevel enum value to transform. </param>
    /// <returns> The file name string representation of the specified SelectLevel enum value. 
    /// </returns>
    public static string TransformLevelToString(SelectLevel level)
    {
        switch (level)
        {
            case SelectLevel.central_mass:
                return "central-mass.txt";
            case SelectLevel.columns:
                return "columns.txt";
            case SelectLevel.level1:
                return "level1.txt";
            case SelectLevel.level2:
                return "level2.txt";
            case SelectLevel.level3:
                return "level3.txt";
            case SelectLevel.wall:
                return "wall.txt";
            case SelectLevel.testlevel:
                return "testlevel.txt";
            default:
                throw new ArgumentException("ERROR - Not a valid stringlevel");
        }
    }
}
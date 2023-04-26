namespace Breakout.Levels;
public static class LevelTransformer {
        public static string TransformLevelToString(SelectLevel level) {
            switch (level){
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
                default:
                    throw new ArgumentException("ERROR - Not a valid stringlevel");
            }
        }
    }
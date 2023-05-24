using DIKUArcade.Events;

namespace Breakout.Levels;
public static class LevelTransformer
{
    /// <summary> Transforms a SelectLevel enum value to its corresponding file name string.
    /// </summary>
    /// <param name="level"> The SelectLevel enum value to transform. </param>
    /// <returns> The file name string representation of the specified SelectLevel enum value. 
    /// </returns>
    public static string TransformLevelToString(SelectLevel level) {
        switch (level) {
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
            case SelectLevel.level4:
                return "level4.txt";
            default:
                throw new ArgumentException("ERROR - Not a valid stringlevel");
        }
    }

    public static SelectLevel TransformIntToLevel(int numericLevel) {
        switch (numericLevel) {
            case 1:
                return SelectLevel.level1;
            case 2:
                return SelectLevel.level2;
            case 3:
                return SelectLevel.level3;
            case 4:
                return SelectLevel.level4;
            // BONUS REMOVE IN FINAL VERSION FOR TA TO TEST LEVELS
            case 5:
                return SelectLevel.central_mass;
            case 6:
                return SelectLevel.columns;
            case 7: 
                return SelectLevel.wall;
            default:
                BreakoutBus.GetBus().RegisterEvent(
                                    new GameEvent {
                                        EventType = GameEventType.GameStateEvent, 
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "GAME_WON"
                                    });
                return SelectLevel.level1;
        }
    }
}
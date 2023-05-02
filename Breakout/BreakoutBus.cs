using DIKUArcade.Events;
namespace Breakout;
public static class BreakoutBus {
    private static GameEventBus eventBus;

    /// <summary> Returns the global event bus instance for the game, and creates one if none exist.
    /// </summary>
    /// <returns> The global event bus instance. </returns>
    public static GameEventBus GetBus() {
        return BreakoutBus.eventBus ?? (BreakoutBus.eventBus = new GameEventBus());
    }
}
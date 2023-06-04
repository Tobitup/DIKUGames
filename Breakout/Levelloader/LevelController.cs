using DIKUArcade.Events;
using DIKUArcade.Entities;
using Breakout.Blocks;

namespace Breakout.Levels;

public static class LevelController{

    /// <summary> 
    /// Switches to a new level by setting the current level to the loaded level. 
    /// </summary>
    public static void SwitchLevel(SelectLevel newlevel) {
        BreakoutStates.GameRunning gameRunning = BreakoutStates.GameRunning.GetInstance();
        gameRunning.LevelLoader = new LevelLoader(newlevel);
        gameRunning.CurrentLevel = gameRunning.LevelLoader.Level;
    }

    public static void incrementLevel() {
        BreakoutStates.GameRunning gameRunning = BreakoutStates.GameRunning.GetInstance();
        gameRunning.NumericLevel += 1;
        SwitchLevel(LevelTransformer.TransformIntToLevel(gameRunning.NumericLevel));
    }

    public static void ChangeLevelIfWon(EntityContainer<Entity> blockContainer) {
        int unbreakables = 0;
        foreach (IBlock block in blockContainer) {
                if (block is UnbreakableBlock){
                    unbreakables++;
                }
        }
        if (blockContainer.CountEntities()-unbreakables==0){
            incrementLevel();
        }
        }

    public static void LoseIfGameLost() {
        BreakoutStates.GameRunning gameRunning = BreakoutStates.GameRunning.GetInstance();
        GameEventBus eventBus;
        eventBus = BreakoutBus.GetBus();
        if (gameRunning.LevelLives.GetCurrentLives == 0 || gameRunning.CurrentLevel.HasTime && 
                                                        gameRunning.CurrentLevel.Timer.IsDead()) {
            gameRunning.GameLost = true;
        } if (gameRunning.GameLost) {
            eventBus.RegisterEvent(
            new GameEvent {
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_LOST" });            
        }
    }
}
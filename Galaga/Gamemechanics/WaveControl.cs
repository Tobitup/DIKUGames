using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga.Squadron;
using Galaga.MovementStrategy;
using System.Collections.Generic;
using System;

namespace Galaga;
public class WaveControl {
    private ISquadron activeSquadron;
    public ISquadron ActiveSquadron {
        get {return activeSquadron;}
    }
    private IMovementStrategy activeStrategy;
    public IMovementStrategy ActiveStrategy {
        get {return activeStrategy;}
    }

    private List<Image> neutralImage;
    private List<Image> enrageImage;

    private Score scoreboard = new Score("Level: 0", new Vec2F(0.05f,0.5f), new Vec2F(0.3f,0.3f));

    public Score Scoreboard {
        get {return scoreboard;}
    }
    private float difficulty = 1.0f;

    private ISquadronFactory randomSquadronFactory = new RandomSquadronFactory();
    private IStrategyFactory randomStrategyFactory = new RandomStrategyFactory();
    
    public WaveControl(List<Image> neutralImage, List<Image> enrageImage) {
        this.neutralImage = neutralImage;
        this.enrageImage = enrageImage;
        activeStrategy = randomStrategyFactory.CreateNewStrategy();
        activeSquadron = randomSquadronFactory.CreateNewSquadron();
    }

    /// <summary> Generates the next wave after all enemies are dead, and increments
    ///           difficulty and score </summary>
    /// <param = enemies> An EntityContainer holding all enemies </param>
    /// <returns> Void </returns> 
    public void GenerateWave(EntityContainer<Enemy> enemies) {
        if (enemies.CountEntities() == 0) {
            activeStrategy = randomStrategyFactory.CreateNewStrategy();
            activeSquadron = randomSquadronFactory.CreateNewSquadron();
            activeSquadron.CreateEnemies(neutralImage,enrageImage);
            enemies = activeSquadron.Enemies;
            difficulty *= 1.1f;
            IncreaseDifficultyForEnemies(enemies);
            scoreboard.IncrementScore();
        }
    }
    /// <summary> Increases the difficulty of the wave and is called in "GenerateWave" </summary>
    /// <param = enemies> An EntityContainer holding all enemies </param>
    /// <returns> Void </returns> 
    public void IncreaseDifficultyForEnemies(EntityContainer<Enemy> enemies) {
        enemies.Iterate( enemy => {
            enemy.IncreaseMovementSpeedByFactor(difficulty);
        });
    }
}
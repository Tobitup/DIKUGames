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
    
    System.Random rnd = new Random();

    public WaveControl(List<Image> neutralImage, List<Image> enrageImage) {
        this.neutralImage = neutralImage;
        this.enrageImage = enrageImage;
        activeStrategy = randomStrategyFactory.CreateNewStrategy();
        activeSquadron = randomSquadronFactory.CreateNewSquadron();
    }

    public void generateWave(EntityContainer<Enemy> enemies) {
        if (enemies.CountEntities() == 0) {
            activeStrategy = randomStrategyFactory.CreateNewStrategy();
            activeSquadron = randomSquadronFactory.CreateNewSquadron();
            activeSquadron.CreateEnemies(neutralImage,enrageImage);
            enemies = activeSquadron.Enemies;
            difficulty *= 1.1f;
            increaseDifficultyForEnemies(enemies);
            scoreboard.IncrementScore();
        }
    }

    public void increaseDifficultyForEnemies(EntityContainer<Enemy> enemies) {
        enemies.Iterate( enemy => {
            enemy.IncreaseMovementSpeedByFactor(difficulty);
        });
    }
}
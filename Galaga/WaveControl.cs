using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga.Squadron;
using Galaga.MovementStrategy;
using System.Collections.Generic;
using System;

namespace Galaga;


public class WaveControl
{
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
    
    System.Random rnd = new Random();

    public WaveControl(List<Image> neutralImage, List<Image> enrageImage) {
        this.neutralImage = neutralImage;
        this.enrageImage = enrageImage;
        activeStrategy = getRandomStrategy();
        activeSquadron = getRandomSquadron();
    }

    private IMovementStrategy getRandomStrategy() {
        switch (rnd.Next(3)) {
            case 1:
                return new Down();
            case 2:
                return new ZigZagDown();
            default:
                return new NoMove();
        }
    }

    private ISquadron getRandomSquadron() {
        switch (rnd.Next(3)) {
            case 1:
                return new SmileySquadron();
            case 2:
                return new SquareSquadron();
            default:
                return new CrossSquadron();
        }
    }

    public void generateWave(EntityContainer<Enemy> enemies) {
        if (enemies.CountEntities() == 0) {
            activeStrategy = getRandomStrategy();
            activeSquadron = getRandomSquadron();
            activeSquadron.CreateEnemies(neutralImage,enrageImage);
            enemies = activeSquadron.Enemies;
            difficulty *= 1.1f;
            increaseDifficultyForEnemies(enemies);
            scoreboard.IncrementScore();
        }
    }

    public void increaseDifficultyForEnemies(EntityContainer<Enemy> enemies) {
        enemies.Iterate( enemy => {
            enemy.increaseDifficulty(difficulty);
        });
    }
}
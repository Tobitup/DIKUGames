using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.Squadron;

public class SquareSquadron : ISquadron {

    private int maxEnemies = 4;
    private EntityContainer<Enemy> enemyContainer;
    public int MaxEnemies {
        get {return maxEnemies;}
    }
    
    public EntityContainer<Enemy> Enemies {
        get {return enemyContainer;}
    }

    public SquareSquadron() {
        enemyContainer = new EntityContainer<Enemy>(maxEnemies);
    }

    public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride)
    {
            // TOP LEFT
            enemyContainer.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStride), alternativeEnemyStride));

            // TOP RIGHT
            enemyContainer.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(0.2f, 0.9f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStride), alternativeEnemyStride));

            // BOTTOM LEFT
            enemyContainer.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(0.1f, 0.8f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStride), alternativeEnemyStride));

            // BOTTOM RIGHT
            enemyContainer.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(0.2f, 0.8f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStride), alternativeEnemyStride));
    }
}
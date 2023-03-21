using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.Squadron;

public class SmileySquadron : ISquadron {

    private int maxEnemies = 4;
    private EntityContainer<Enemy> enemyContainer;
    public int MaxEnemies {
        get {return maxEnemies;}
    }
    
    public EntityContainer<Enemy> Enemies {
        get {return enemyContainer;}
    }

    public SmileySquadron() {
        enemyContainer = new EntityContainer<Enemy>(maxEnemies);
    }

    public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride)
    {
            // ROW ONE
            enemyContainer.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(0.2f, 0.9f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStride), new ImageStride(80 ,alternativeEnemyStride)));

            // ROW ONE
            enemyContainer.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(0.4f, 0.9f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStride), new ImageStride(80 ,alternativeEnemyStride)));

            // ROW TWO
            enemyContainer.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(0.1f, 0.7f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStride), new ImageStride(80 ,alternativeEnemyStride)));

            // ROW TWO
            enemyContainer.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(0.5f, 0.7f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStride), new ImageStride(80 ,alternativeEnemyStride)));

            // ROW THREE
            enemyContainer.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(0.2f, 0.6f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStride), new ImageStride(80 ,alternativeEnemyStride)));

            // ROW THREE
            enemyContainer.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(0.3f, 0.6f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStride), new ImageStride(80 ,alternativeEnemyStride)));

            // ROW THREE
            enemyContainer.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(0.4f, 0.6f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, enemyStride), new ImageStride(80 ,alternativeEnemyStride)));
    }
}
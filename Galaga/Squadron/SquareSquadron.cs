using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.Squadron;

public class SquareSquadron : ISquadron {

    private int maxEnemies = 4;
    private EntityContainer<Enemy> enemyContainer;
    public EntityContainer<Enemy> Enemies {
        get {return enemyContainer;}
    }

    public SquareSquadron() {
        enemyContainer = new EntityContainer<Enemy>(maxEnemies);
    }

    /// <summary> Creates a SqareSquadron </summary>
    /// <param = enemyStride> The current enemy Image asset </param>
    /// <param = alternativeEnemyStride> The alternate enemy Image asset </param>
    /// <returns> Void </returns> 
    public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
        // TOP LEFT
        enemyContainer.AddEntity(new Enemy(
        new DynamicShape(new Vec2F(0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
        new ImageStride(80, enemyStride), new ImageStride(80 ,alternativeEnemyStride)));

        // TOP RIGHT
        enemyContainer.AddEntity(new Enemy(
        new DynamicShape(new Vec2F(0.2f, 0.9f), new Vec2F(0.1f, 0.1f)),
        new ImageStride(80, enemyStride), new ImageStride(80 ,alternativeEnemyStride)));

        // BOTTOM LEFT
        enemyContainer.AddEntity(new Enemy(
        new DynamicShape(new Vec2F(0.1f, 0.8f), new Vec2F(0.1f, 0.1f)),
        new ImageStride(80, enemyStride), new ImageStride(80 ,alternativeEnemyStride)));

        // BOTTOM RIGHT
        enemyContainer.AddEntity(new Enemy(
        new DynamicShape(new Vec2F(0.2f, 0.8f), new Vec2F(0.1f, 0.1f)),
        new ImageStride(80, enemyStride), new ImageStride(80 ,alternativeEnemyStride)));
    }
}
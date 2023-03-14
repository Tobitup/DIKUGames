using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;

namespace Galaga.MovementStrategy;

public class Down : IMovementStrategy {

    private EntityContainer<Enemy> enemies;

    public void MoveEnemies(EntityContainer<Enemy> enemies)
    {
        foreach (Enemy enemy in enemies) {
            MoveEnemy(enemy);
        }
    }

    public void MoveEnemy(Enemy enemy)
    {
        float currentPosition = enemy.Shape.Position.Y;
        float newPosition = currentPosition - enemy.MovementSpeed;
        enemy.Shape.Position.Y = newPosition;
    }
}
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;

namespace Galaga.MovementStrategy;

public class NoMove : IMovementStrategy {

    private EntityContainer<Enemy> enemies;
    public NoMove(EntityContainer<Enemy> enemies) {
        this.enemies = enemies;
    }

    public void MoveEnemies(EntityContainer<Enemy> enemies)
    {
    }

    public void MoveEnemy(Enemy enemy)
    {
    }
}
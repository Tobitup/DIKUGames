using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;

namespace Galaga.MovementStrategy {
public interface IMovementStrategy {
    void MoveEnemy (Enemy enemy);
    void MoveEnemies (EntityContainer<Enemy> enemies);
    }
}
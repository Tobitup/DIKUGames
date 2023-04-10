using DIKUArcade.Entities;
namespace Galaga.MovementStrategy;

public class Down : IMovementStrategy {
    public void MoveEnemies(EntityContainer<Enemy> enemies) {
        foreach (Enemy enemy in enemies) {
            MoveEnemy(enemy);
        }
    }

    /// <summary> Updates all enemys Y position to move downwards </summary>
    /// <param = enemy> Individual enemy in an enemyContainer of type Enemy </param>
    /// <returns> Void </returns> 
    public void MoveEnemy(Enemy enemy) {
        float currentPosition = enemy.Shape.Position.Y;
        float newPosition = currentPosition - enemy.MovementSpeed;
        enemy.Shape.Position.Y = newPosition;
    }
}
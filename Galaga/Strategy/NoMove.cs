using DIKUArcade.Entities;
namespace Galaga.MovementStrategy;

public class NoMove : IMovementStrategy {
    public void MoveEnemies(EntityContainer<Enemy> enemies) {
    }
    
    /// <summary> Void method to move enemies </summary>
    /// <param = enemy> Individual enemy in an enemyContainer of type Enemy </param>
    /// <returns> Void </returns> 
    public void MoveEnemy(Enemy enemy) {
    }
}
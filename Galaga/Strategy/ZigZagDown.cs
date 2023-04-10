using DIKUArcade.Entities;
using DIKUArcade.Math;
using System;

namespace Galaga.MovementStrategy;

public class ZigZagDown : IMovementStrategy {
    const float s = 0.0003f;
    const float p = 0.045f;
    const float a = 0.05f;
    const float pi = (float)Math.PI;

    public void MoveEnemies(EntityContainer<Enemy> enemies) {
        foreach (Enemy enemy in enemies) {
            MoveEnemy(enemy);
        }
    }

    public void MoveEnemy(Enemy enemy) {
        enemy.Shape.Position = CalculateNextPosition(enemy);
    }

    private float GetSpeed(Enemy enemy) {
        return s + enemy.MovementSpeed;
    }

    /// <summary> Calculates the enemy's next position </summary>
    /// <param = enemy> Individual enemy in an enemyContainer of type Enemy </param>
    /// <returns> A Vec2F possition </returns> 
    private Vec2F CalculateNextPosition(Enemy enemy) {
        Vec2F returnPosition = new Vec2F(0.0f,0.0f);
        returnPosition.Y = CalculateNextYPosition(enemy);
        returnPosition.X = CalculateNextXPosition(enemy,returnPosition.Y);
        return returnPosition;
    }
    /// <summary> Calculate the enemy's next Y position </summary>
    /// <param = enemy> Individual enemy in an enemyContainer of type Enemy </param>
    /// <returns> A float for enemys new Y position </returns> 
    private float CalculateNextYPosition(Enemy enemy) {
        Vec2F currentPosition = enemy.Shape.Position;

        return currentPosition.Y - GetSpeed(enemy);
    }

    /// <summary> Calculate the enemy's next X position </summary>
    /// <param = enemy> Individual enemy in an enemyContainer of type Enemy </param>
    /// <param = nextYPos> The next Y position calculated above of type float </param>
    /// <returns> A float for enemys new X position </returns> 
    private float CalculateNextXPosition(Enemy enemy, float nextYPos) {
        float startPosX = enemy.Startposition.X;
        float startPosY = enemy.Startposition.Y;

        float nextXPosition = startPosX + a*(float)Math.Sin((2.0f*pi*(startPosY - nextYPos))/p);
        return nextXPosition;
    }
}
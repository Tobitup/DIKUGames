using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;
using System.Collections.Generic;

namespace Galaga.MovementStrategy;

public class ZigZagDown : IMovementStrategy {
    const float s = 0.0003f;
    const float p = 0.045f;
    const float a = 0.05f;
    const float pi = (float)Math.PI;

    public void MoveEnemies(EntityContainer<Enemy> enemies)
    {
        foreach (Enemy enemy in enemies) {
            MoveEnemy(enemy);
        }
    }

    public void MoveEnemy(Enemy enemy)
    {
        enemy.Shape.Position = CalculateNextPosition(enemy);
    }

    private float GetSpeed(Enemy enemy) {
        return s + enemy.MovementSpeed;
    }

    private Vec2F CalculateNextPosition(Enemy enemy) {
        Vec2F returnPosition = new Vec2F(0.0f,0.0f);
        returnPosition.Y = CalculateNextYPosition(enemy);

        returnPosition.X = CalculateNextXPosition(enemy,returnPosition.Y);

        return returnPosition;
    }

    private float CalculateNextYPosition(Enemy enemy) {
        Vec2F currentPosition = enemy.Shape.Position;

        return currentPosition.Y - GetSpeed(enemy);
    }

    private float CalculateNextXPosition(Enemy enemy, float nextYPos) {
        float startPosX = enemy.Startposition.X;
        float startPosY = enemy.Startposition.Y;

        float nextXPosition = startPosX + a*(float)Math.Sin((2.0f*pi*(startPosY - nextYPos))/p);
        return nextXPosition;
    }
}
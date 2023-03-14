using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System;
using System.Collections.Generic;

namespace Galaga.MovementStrategy;

public class ZigZagDown : IMovementStrategy {

    private EntityContainer<Enemy> enemies;
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
        Vec2F currentPosition = enemy.Shape.Position;
        //float newPosition = currentPosition - enemy.MovementSpeed;
        enemy.Shape.Position = calculateNextPosition(currentPosition);
    }

    private Vec2F calculateNextPosition(Vec2F currentPosition) {
        Vec2F returnPosition = new Vec2F(0.0f,0.0f);
        returnPosition.Y = currentPosition.Y - s;

        float nextXPosition = 0.5f + a*(float)Math.Sin((2.0f*pi*(0.0f-returnPosition.Y))/p);
        returnPosition.X = nextXPosition;

        return returnPosition;
    }
}
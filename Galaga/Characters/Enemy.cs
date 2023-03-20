using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using System;

namespace Galaga;
public class Enemy : Entity {
    private int hitpoints;
    private float movementSpeed = 0.001f;
    public float MovementSpeed {
        get {return movementSpeed;}
    }

    private IBaseImage redEnemies;

    private int enrageHPThreshold;

    private Vec2F startposition;
    public Vec2F Startposition {
        get {return startposition;}
    }
    
    public Enemy(DynamicShape shape, IBaseImage image, List<Image> imageStride) : base(shape, image) {
        Random rnd = new Random();
        hitpoints = rnd.Next(5);
        enrageHPThreshold = (int)Math.Ceiling(hitpoints/2.0);

        redEnemies = new ImageStride(80, imageStride);
        startposition = shape.Position;
    }

    public bool EnemyIsTakingDamage() {
        hitpoints--;
        if (hitpoints <= 0) {
            return false;
        }
        if (hitpoints <= enrageHPThreshold) {
            EnrageEnemy();
        }
        return true;
    }

    public void increaseDifficulty(float difficulty) {
        movementSpeed *= difficulty;
    }

    public void EnrageEnemy() {
        base.Image = redEnemies;
        movementSpeed = 0.01f;
    }
}
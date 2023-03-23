using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using System;

namespace Galaga;
public class Enemy : Entity {
    private int hitpoints;

    // Made to access Hitpoints in Testing class
    public int Hitpoints {
        get{return hitpoints;}
    }
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
    
    public Enemy(DynamicShape shape, IBaseImage image, IBaseImage redImageStride) : base(shape, image) {
        Random rnd = new Random();
        hitpoints = rnd.Next(5);
        enrageHPThreshold = (int)Math.Ceiling(hitpoints/2.0);

        redEnemies = redImageStride;
        startposition = shape.Position;
    }

    public void TakeDamage() {
        hitpoints--;
    }

    public bool IsDead() {
        if (hitpoints <= 0) {
            return true;
        }
        return false;
    }

    public void GetsShot() {
        TakeDamage();
        if (hitpoints <= enrageHPThreshold) {
            EnrageEnemy();
        }
    }

    public void IncreaseMovementSpeedByFactor(float factor) {
        movementSpeed *= factor;
    }

    public void EnrageEnemy() {
        base.Image = redEnemies;
        movementSpeed = 0.01f;
    }
}
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;

namespace Galaga;
public class Enemy : Entity {
    private int hitpoints;
    private float movementSpeed = 0.01f;
    private IBaseImage redEnemies;

    private int enrageHPThreshold = 1;

    public Enemy(DynamicShape shape, IBaseImage image, List<Image> imageStride) : base(shape, image) {
        hitpoints = 3;
        redEnemies = new ImageStride(80, imageStride);
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

    public void EnrageEnemy() {
        base.Image = redEnemies;
        movementSpeed -= 0.001f;
    }

}
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
namespace Galaga.Squadron {
public interface ISquadron {
    EntityContainer<Enemy> Enemies {get;}
    void CreateEnemies (List<Image> enemyStride,
        List<Image> alternativeEnemyStride);
    }
}
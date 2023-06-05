using DIKUArcade.Entities;

namespace Breakout.Blocks
{
    /// <summary>
    /// Interface for Blocks
    /// </summary>
    public interface IBlock {
        uint Value { get; }
        void TakeDamage();
        void Update();
        bool IsDead();
        void RemoveIfDead();
        Shape Shape {get;}
        int HitPoints {get;}
    }
}
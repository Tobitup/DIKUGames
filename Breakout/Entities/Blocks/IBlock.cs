namespace Breakout.Blocks
{
    public interface IBlock
    {
        uint Value { get; }
        void TakeDamage();
        void Update();
        bool IsDead();
        void RemoveIfDead();
    }
}
using Galaga.MovementStrategy;

namespace Galaga;

public interface IStrategyFactory {
    public IMovementStrategy CreateNewStrategy();
}
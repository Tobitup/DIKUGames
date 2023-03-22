using Galaga.MovementStrategy;
using System;
namespace Galaga;

public class RandomStrategyFactory: IStrategyFactory {

    private Random rnd = new Random();
    public IMovementStrategy CreateNewStrategy() {
        switch (rnd.Next(3)) {
            case 1:
                return new Down();
            case 2:
                return new ZigZagDown();
            default:
                return new NoMove();
        }
    }
}
using Galaga.Squadron;
using System;
namespace Galaga;

public class RandomSquadronFactory: ISquadronFactory {

    private Random rnd = new Random();
    public ISquadron CreateNewSquadron() {
        switch (rnd.Next(3)) {
            case 1:
                return new SmileySquadron();
            case 2:
                return new SquareSquadron();
            default:
                return new CrossSquadron();
        }
    }
}
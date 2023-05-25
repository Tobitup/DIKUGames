using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Math;
using System.IO;

namespace Breakout.GameTimer;
public class BigJimTimer /*: ITimer*/ {
    private static BigJimTimer instance = null;

    public static BigJimTimer GetInstance() {
        if (BigJimTimer.instance == null) {
            BigJimTimer.instance = new BigJimTimer();
            }
        return BigJimTimer.instance;
    }

    public void getDuration()
    {
        throw new NotImplementedException();
    }

    public void reset()
    {
        throw new NotImplementedException();
    }

    public void setDuration()
    {
        throw new NotImplementedException();
    }

    public void start()
    {
        throw new NotImplementedException();
    }
}
namespace Breakout.GameTimer;

public interface ITimer {

    void start();
    void reset();
    void setDuration();
    void getDuration();
}
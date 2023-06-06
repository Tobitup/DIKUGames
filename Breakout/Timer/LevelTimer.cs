using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;

namespace Breakout.GameTimer;


public class LevelTimer {
    private int duration;
    private int durationDelta;
    private Text timerLabel;
    public Text TimerLabel {get {return timerLabel; }}
    
    /// <summary> Initializes a new instance of the Timer object. </summary>
    public LevelTimer() {
        duration = -1;
        timerLabel = new Text("", new Vec2F(0.05f,0.35f), new Vec2F(0.3f,0.3f));
        timerLabel.SetColor(new Vec3I(255,255,255));
        StaticTimer.RestartTimer();
    }

    /// <summary> Updates the text object with how much time is left. </summary>
    private void UpdateLabel() {
        timerLabel.SetText($"Time: {durationDelta}");
    }

    /// <summary> Sets the timer to an amount of seconds. </summary>
    /// <param name="duration"> The amount to set the timer to. </param>
    public void SetDuration(int duration) {
        this.duration = duration;
    }

    /// <summary> Updates the timer with the elapsed seconds. </summary>
    public void UpdateTime() {
        if (duration != -1) {
            durationDelta = duration - (int)StaticTimer.GetElapsedSeconds();
            IsDead();
            UpdateLabel();
        }
    }

    /// <summary> Checks if the seconds in the duration have elapsed. </summary>
    public bool IsDead() {
        if (durationDelta <= 0) {
            return true;
        }
        else {return false;}
    }
}
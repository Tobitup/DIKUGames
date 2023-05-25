using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;

namespace Breakout.GameTimer;


public class LevelTimer {
    private int duration;
    private int durationDelta;

    private Text timerLabel;
    public Text TimerLabel {get {return timerLabel; }}
    
    public LevelTimer() {
        duration = -1;
        timerLabel = new Text("", new Vec2F(0.05f,0.35f), new Vec2F(0.3f,0.3f));
        timerLabel.SetColor(new Vec3I(255,255,255));
        StaticTimer.RestartTimer();
    }

    private void UpdateLabel() {
        timerLabel.SetText($"Time: {durationDelta}");
    }

    public void SetDuration(int duration) {
        this.duration = duration;
    }

    public void UpdateTime() {
        if (duration != -1) {
            durationDelta = duration - (int)StaticTimer.GetElapsedSeconds();
            IsDead();
            UpdateLabel();
        }
    }

    public bool IsDead() {
        if (durationDelta <= 0) {
            return true;
            //throw new Exception("GØR NOGET SEJT?");
        }
        else {return false;}
    }




}
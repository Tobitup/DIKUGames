using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.PlayerScore;

public class Score : Text{
    private uint score = 0;

    public Score() : base("Score: 0", new Vec2F(0.05f,0.45f), new Vec2F(0.5f,0.5f)) {
        this.SetColor(new Vec3I(255,255,255));
    }

    public void IncrementScore(uint reward) {
        score += reward;
        this.SetText($"Score: {score}");
    }

    public uint GetCurrentScore() {
        return score;
    }
    public void ResetScore() {
        score = 0;
    }
}
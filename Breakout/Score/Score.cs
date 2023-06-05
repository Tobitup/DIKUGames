using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.PlayerScore;

public class Score : Text{
    private uint score = 0;

    public uint GetCurrentScore { get {return score;}}

    /// <summary> Initializes a new instance of the Score object 
    ///</summary>
    public Score() : base("Score: 0", new Vec2F(0.05f,0.45f), new Vec2F(0.3f,0.3f)) {
        this.SetColor(new Vec3I(255,255,255));
    }

    /// <summary> Adds an amount to the score  </summary>
    /// <param name="reward"> The amount to add to the score </param>
    public void IncrementScore(uint reward) {
        score += reward;
        this.SetText($"Score: {score}");
    }

    /// <summary> resets score to zero, used for testing purposes  </summary>
    public void ResetScore() {
        score = 0;
    }
}
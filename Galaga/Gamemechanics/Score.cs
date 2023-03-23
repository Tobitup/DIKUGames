namespace Galaga;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Score : Text{
    private static int count = 0;

    public Score(string text, Vec2F pos, Vec2F extent) : base(text, pos, extent)
    {
        this.SetColor(new Vec3I(255,255,255));
    }

    public void IncrementScore() {
        count += 1;
        this.SetText($"Level: {count}");
    }

    public static int GetCurrentScore() {
        return count;
    }
    public void ResetScore() {
        count = 0;
    }
}
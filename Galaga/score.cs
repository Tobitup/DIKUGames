namespace Galaga;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

class Score : Text{
    private int count = 0;

    public Score(string text, Vec2F pos, Vec2F extent) : base(text, pos, extent)
    {
        this.SetColor(new Vec3I(255,255,255));
    }

    public void IncrementScore() {
        count += 1;
        this.SetText($"{count}");
    }
}
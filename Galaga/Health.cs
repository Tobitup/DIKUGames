using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;
public class Health {
    private int health;
    private Text display;
    private bool isDead = false;

    // Used to check if the game is over.
    public bool IsDead {
        get {return isDead;}
    }
    public Health (Vec2F position, Vec2F extent) {
        health = 3;
        display = new Text (health.ToString(), position, extent);
        display.SetColor(new Vec3I(255,255,255));
    }

    // Method used to check if the player's remaining health is less than or equal to zero
    // if so, the isDead bool is updated and the game will end.
    public void LoseHealth () {
        if (health <= 0) {
            isDead = true;
        } else {
            health--;
        }
    }
    // Used to render the health text to the window.
    public void RenderHealth () {
        display.SetText($"HP: {health}");
        display.RenderText();
    }
}
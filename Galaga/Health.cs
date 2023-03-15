using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Galaga;
public class Health {
    private int health;
    private Text display;
    public Health (Vec2F position, Vec2F extent) {
        health = 3;
        display = new Text (health.ToString(), position, extent);
        display.SetColor(new Vec3I(255,255,255));
    }
    // Remember to explaination your choice as to what happens //when losing health.
    public void LoseHealth () {
        
    }
    public void RenderHealth () {
        display.RenderText();
    }
}
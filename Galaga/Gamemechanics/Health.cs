using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Galaga;
public class Health {
    private int health;
    private string healthbar;
    private int startingHealth = 3;
    public int readHealth {
        get{return health;}
    }
    private Text display;
  

    public Health (Vec2F position, Vec2F extent) {
        health = startingHealth;
        display = new Text (health.ToString(), position, extent);
        display.SetColor(new Vec3I(255,255,255));
    }

    public void LoseHealth () {
        health--;
    }

    public void IsDead() {
        if (health <= 0) {
            GalagaBus.GetBus().RegisterEvent(
                                    new GameEvent{
                                        EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "GAME_OVER"
                                    });
        }
    }

    // Used to render the health text to the window.
    public void RenderHealth () {
        //display.SetText($"Lives: {health}");
        UpdateHealthBar();
        display.SetText(healthbar);
        display.SetColor(new Vec3I(255,0,0));
        display.RenderText();
    }

    public void UpdateHealthBar() {
        healthbar = "";
        for (int i = 1; i<=health; i++) {
            healthbar += "❤️";
        }
    }

    public void resetHealth() {
        health = startingHealth;
    }
}
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Math;
using System.IO;

namespace Galaga.GalagaStates;
public class GameLost : IGameState {
    private static GameLost instance = null;
    private static Text gameOver = new Text("GAME OVER", new Vec2F(0.3f,0.4f), 
                                                                new Vec2F(0.5f,0.5f));
    private Text[] menuButtons = {new Text("NEW GAME", new Vec2F(0.25f,0.15f), 
            new Vec2F(0.5f,0.5f)), new Text("QUIT", new Vec2F(0.25f,0.0f), new Vec2F(0.5f,0.5f))};
    private int activeMenuButton;
    private static Text levelReached;
    private Entity backGroundImage;
    public static GameLost GetInstance() {
        if (GameLost.instance == null) {
                GameLost.instance = new GameLost();
                GameLost.instance.InitializeGameState();
            }
        return GameLost.instance;
    }

    private void InitializeGameState() {
        levelReached = new Text($"Score: {Score.GetCurrentScore()}", new Vec2F(0.4f,0.3f), 
                                                                            new Vec2F(0.5f, 0.5f));
        gameOver.SetColor(new Vec3I(255,255,255));
        levelReached.SetColor(new Vec3I(255,255,255));

        backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f),
                                            new Vec2F(1.0f,1.0f)),new Image(Path.Combine("Assets",
                                                                    "Images", "TitleImage.png")));
        
        menuButtons[0].SetColor(new Vec3I(255,255,255));
        menuButtons[0].SetFont("Impact");
        menuButtons[1].SetFont("Impact");
        menuButtons[1].SetColor(new Vec3I(255,255,255));
        activeMenuButton = 0;
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyPress) {
            switch (key) {
                case KeyboardKey.Up:
                    activeMenuButton = 0;
                    break;
                case KeyboardKey.Down:
                    activeMenuButton = 1;
                    break;
                case KeyboardKey.Enter:
                    switch (activeMenuButton) {
                        case 0:
                            GalagaBus.GetBus().RegisterEvent(
                                new GameEvent{
                                    EventType = GameEventType.GameStateEvent,
                                    Message = "CHANGE_STATE",
                                    StringArg1 = "GAME_RUNNING"
                                });
                            GalagaBus.GetBus().RegisterEvent(
                                new GameEvent{
                                    EventType = GameEventType.GameStateEvent,
                                    Message = "RESET_STATE",
                                    });
                            break;
                        case 1:
                            GalagaBus.GetBus().RegisterEvent(new GameEvent {
                                                            EventType = GameEventType.WindowEvent, 
                                                                        Message = "CLOSE_WINDOW"});
                            break;
                        default:
                            break;
                    }
                    break;
            }
        }
    }

    public void RenderState() {
        backGroundImage.RenderEntity();
        levelReached.RenderText();
        gameOver.RenderText();
        menuButtons[0].RenderText();
        menuButtons[1].RenderText();
    }

    public void ResetState() {}

    public void UpdateState() {
        levelReached.SetText($"Level {Score.GetCurrentScore()}");
        switch (activeMenuButton) {
                case 0:
                    menuButtons[0].SetColor(new Vec3I(0,255,0));
                    menuButtons[1].SetColor(new Vec3I(255,255,255));
                break;
                case 1:
                    menuButtons[1].SetColor(new Vec3I(0,255,0));
                    menuButtons[0].SetColor(new Vec3I(255,255,255));
                    break;
                default:
                break;
            }
    }
}

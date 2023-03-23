using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Math;
using System.IO;

namespace Galaga.GalagaStates;
public class MainMenu : IGameState {
    private static MainMenu instance = null;
    private Entity backGroundImage;
    private Text[] menuButtons = {new Text("NEW GAME", new Vec2F(0.25f,0.15f), new Vec2F(0.5f,0.5f)),
                                    new Text("QUIT", new Vec2F(0.25f,0.0f), new Vec2F(0.5f,0.5f))};
    private int activeMenuButton;
    private int maxMenuButtons;
    public static MainMenu GetInstance() {
        if (MainMenu.instance == null) {
            MainMenu.instance = new MainMenu();
            MainMenu.instance.InitializeGameState();
            }
        return MainMenu.instance;
        }

        private void InitializeGameState() {
            menuButtons[0].SetColor(new Vec3I(255,255,255));
            menuButtons[1].SetColor(new Vec3I(255,255,255));
            menuButtons[0].SetFont("Impact");
            menuButtons[1].SetFont("Impact");
            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f),
                                            new Vec2F(1.0f,1.0f)),new Image(Path.Combine("..",
                                                "Galaga","Assets","Images", "TitleImage.png")));
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

                            break;
                            case 1:
                            GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.WindowEvent, 
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
            menuButtons[0].RenderText();
            menuButtons[1].RenderText();
        }

        public void ResetState() {
            MainMenu.instance.InitializeGameState();
        }

        public void UpdateState() {
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
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Math;
using System.IO;

namespace Breakout.BreakoutStates;
public class GameWon : IGameState {
    private static GameWon instance = null;
    private Entity backGroundImage;
    private int activeMenuButton;

    private uint finalScore = 0;
    private Text finalScoreText = new Text ("SCORE:",new Vec2F(0.25f,-0.20f), new Vec2F(0.8f,0.8f));
    private Text gameOverText = new Text("GAME WON :D",new Vec2F(0.25f,-0.05f), new Vec2F(0.8f,0.8f));
    private Text[] menuButtons = {new Text("MAIN MENU",new Vec2F(0.25f,-0.10f), new Vec2F(0.5f,0.5f)),
                                new Text("QUIT",new Vec2F(0.25f,-0.20f), new Vec2F(0.5f,0.5f))};


    /// <summary> Gets the singleton instance of the GameLost state. </summary>
    /// <returns> The GameLost instance. </returns>
    public static GameWon GetInstance() {
        if (GameWon.instance == null) {
            GameWon.instance = new GameWon();
            GameWon.instance.InitializeGameState();
            }
        return GameWon.instance;
    }

    /// <summary> Initializes the game state by setting the color and font of menu buttons, 
    ///           creating an entity for the background image, and setting the active menu button.
    /// </summary>
    /// <returns> Void. </returns>
    private void InitializeGameState() {
        finalScore = GameRunning.GetInstance().GetCurrentScore;
        finalScoreText = new Text ($"SCORE: {finalScore}",new Vec2F(0.25f,-0.20f), new Vec2F(0.8f,0.8f));
        menuButtons[0].SetColor(new Vec3I(255,255,255));
        menuButtons[1].SetColor(new Vec3I(255,255,255));
        gameOverText.SetColor(new Vec3I(196,10,28));
        finalScoreText.SetColor(new Vec3I(196,10,28));
        menuButtons[0].SetFont("Impact");
        menuButtons[1].SetFont("Impact");
        gameOverText.SetFont("Impact");
        finalScoreText.SetFont("Impact");
        backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f),
                                new Vec2F(1.0f,1.0f)),new Image(Path.Combine(
                                                            "..","Breakout","Assets",
                                                            "Images", "shipit_titlescreen.png")));
        activeMenuButton = 0;
    }

    /// <summary> In charge of handling Keyboard input from user, along with registering events
    ///           forwarded to the eventbus. </summary>
    /// <param name="action"> The Keybaord Action the Eventhandler listens for. </param>
    /// <param name="key"> The given key the user presses. </param>
    /// <returns> Void </returns>
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
                        BreakoutBus.GetBus().RegisterEvent(
                                new GameEvent {
                                    EventType = GameEventType.GameStateEvent, 
                                    Message = "CHANGE_STATE",
                                    StringArg1 = "MENU"
                                });

                            break;
                        case 1:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent 
                        {EventType = GameEventType.WindowEvent, 
                                        Message = "CLOSE_WINDOW"});
                            break;
                        default:
                            break;
                    }
                    break;
            }
        }
    }

    /// <summary> Renders the current game state, with background and menu buttons. </summary>
    /// <returns> Void. </returns>
    public void RenderState() {
        backGroundImage.RenderEntity();
        menuButtons[0].RenderText();
        menuButtons[1].RenderText();
        gameOverText.RenderText();
        finalScoreText.RenderText();
    }

    /// <summary> Resets the state of the game paused screen to its initial state. </summary>
    /// <returns> Void. </returns>
    public void ResetState() {
        GameWon.instance.InitializeGameState();
    }

    /// <summary> Updates the state of the game paused screen based on the active menu button, and
    ///          sets the color of the active menu button to green and the inactive button to white. 
    /// </summary>
    /// <returns> Void. </returns>
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
using DIKUArcade.State;
using DIKUArcade.Input;

namespace Galaga.GalagaStates;
public class GameWon : IGameState {
    private static GameWon instance = null;
    public static GameWon GetInstance() {
        if (GameWon.instance == null) {
            GameWon.instance = new GameWon();
            GameWon.instance.InitializeGameState();
        }
            return GameWon.instance;
    }

    private void InitializeGameState() {

    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
    {
        throw new System.NotImplementedException();
    }

    public void RenderState()
    {
        throw new System.NotImplementedException();
    }

    public void ResetState()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}

using DIKUArcade.Events;
using DIKUArcade.State;
namespace Galaga.GalagaStates;
public class StateMachine : IGameEventProcessor {
    public IGameState ActiveState { get; private set; }
    public StateMachine() {
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        ActiveState = MainMenu.GetInstance();
    }

    /// <summary> Changes the game States </summary>
    /// <param = stateType> A GameStateType used to update the current game State </param>
    /// <returns> Void </returns> 
    private void SwitchState(GameStateType stateType) {
        switch (stateType) {
            case GameStateType.GamePaused:
                ActiveState = GamePaused.GetInstance();
                break;
            case GameStateType.GameRunning:
                ActiveState = GameRunning.GetInstance();
                break;
            case GameStateType.MainMenu:
                ActiveState = MainMenu.GetInstance();
                break;
            case GameStateType.GameLost:
                ActiveState = GameLost.GetInstance();
                break;
        }
    }

    /// <summary> ProcessEvent for switching States when gameEvent Message recived </summary>
    /// <param = gameEvent> GameEvent type </param>
    /// <returns> Void </returns> 
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.GameStateEvent) {
            if (gameEvent.Message == "CHANGE_STATE") {
                switch (gameEvent.StringArg1) {
                case "GAME_RUNNING":
                    SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
                    break;
                case "GAME_PAUSED":
                    SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
                    break;
                case "GAME_OVER":
                    SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
                    break;
                case "MENU":
                    SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
                    break;
                default:
                    break;
                }
            }
            if (gameEvent.Message == "RESET_STATE") {
                ActiveState.ResetState();
            }
        }
    }
}       

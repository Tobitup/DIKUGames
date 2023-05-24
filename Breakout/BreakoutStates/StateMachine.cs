using DIKUArcade.Events;
using DIKUArcade.State;
namespace Breakout.BreakoutStates;
public class StateMachine : IGameEventProcessor {
    public IGameState ActiveState { get; private set; }
    public StateMachine() {
        BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);
        ActiveState = BreakoutStates.MainMenu.GetInstance();
    }

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
            case GameStateType.GameWon:
                ActiveState = GameWon.GetInstance();
                break;
            
        }
    }

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
                case "MENU":
                    SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
                    break;
                case "GAME_LOST":
                    SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
                    break;
                case "GAME_WON":
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

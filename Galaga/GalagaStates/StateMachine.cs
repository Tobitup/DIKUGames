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
    public void ProcessEvent(GameEvent gameEvent) {
        System.Console.WriteLine(gameEvent.Message);
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

using System;

namespace Galaga.GalagaStates {
    public enum GameStateType {
        GameRunning,
        GamePaused,
        MainMenu,
        GameWon,
        GameLost
    }

    public class StateTransformer {
        public static GameStateType TransformStringToState(string state) {
            switch (state){
                case "GAME_RUNNING":
                    return GameStateType.GameRunning;
                case "GAME_PAUSED":
                    return GameStateType.GamePaused;
                case "MENU":
                    return GameStateType.MainMenu;
                case "GAME_OVER":
                    return GameStateType.GameLost;
                default:
                    throw new ArgumentException("ERROR - Not a valid StringState");
            }
        }

        public static string TransformStateToString(GameStateType state) {
            switch (state){
                case GameStateType.GameRunning:
                    return "GAME_RUNNING";
                case GameStateType.GamePaused:
                    return "GAME_PAUSED";
                case GameStateType.MainMenu:
                    return "MENU";
                case GameStateType.GameLost:
                    return "GAME_OVER";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}


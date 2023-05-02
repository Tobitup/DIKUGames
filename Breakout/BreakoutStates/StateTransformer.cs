using System;

namespace Breakout.BreakoutStates;
public class StateTransformer {

    /// <summary> Transforms a string representation of a GameState into the corresponding 
    ///           GameStateType. </summary>
    /// <param name="state"> The string representation of the GameState. </param>
    /// <returns> The corresponding GameStateType enum. </returns>
    public static GameStateType TransformStringToState(string state) {
        switch (state){
            case "GAME_RUNNING":
                return GameStateType.GameRunning;
            case "GAME_PAUSED":
                return GameStateType.GamePaused;
            case "MENU":
                return GameStateType.MainMenu;
            /* case "GAME_OVER":
                return GameStateType.GameLost;
            */
            default:
                throw new ArgumentException("ERROR - Not a valid StringState");
        }
    }

    /// <summary> Transforms a GameStateType into the corresponding string representation of a 
    ///           GameState </summary>
    /// <param name="state"> The GameStateType to be transformed into its string representation. 
    /// </param>
    /// <returns> The corresponding GameState string representation. </returns>
    public static string TransformStateToString(GameStateType state) {
        switch (state){
            case GameStateType.GameRunning:
                return "GAME_RUNNING";
            case GameStateType.GamePaused:
                return "GAME_PAUSED";
            case GameStateType.MainMenu:
                return "MENU";
            /* case GameStateType.GameLost:
                return "GAME_OVER";
            */
            default:
                throw new NotImplementedException();
        }
    }
}
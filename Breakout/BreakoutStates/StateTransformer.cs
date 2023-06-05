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
            case "GAME_LOST":
                return GameStateType.GameLost;
            case "GAME_WON":
                return GameStateType.GameWon;
            default:
                throw new ArgumentException("ERROR - Not a valid StringState");
                
        }
    }
}
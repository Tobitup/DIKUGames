namespace Breakout.Effect;
public class EffectTransformer {

    /// <summary> Transforms a string representation of a GameState into the corresponding 
    ///           GameStateType. </summary>
    /// <param name="state"> The string representation of the GameState. </param>
    /// <returns> The corresponding GameStateType enum. </returns>
    public static Effects TransformStringToEffect(string effect) {
        switch (effect){
            case "BigJim_START":
                return Effects.BigJim;
            default:
                throw new ArgumentException("ERROR - Not a valid effect");
        }
    }

    /// <summary> Transforms a GameStateType into the corresponding string representation of a 
    ///           GameState </summary>
    /// <param name="state"> The GameStateType to be transformed into its string representation. 
    /// </param>
    /// <returns> The corresponding GameState string representation. </returns>
    public static string TransformEffectToString(Effects effect) {
        switch (effect){
            case Effects.BigJim:
                return "BigJim_START";

            default:
                throw new NotImplementedException();
        }
    }
}
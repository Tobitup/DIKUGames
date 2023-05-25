namespace Breakout.Effect;
public static class EffectTransformer {

    /// <summary> Transforms a string representation of a GameState into the corresponding 
    ///           GameStateType. </summary>
    /// <param name="state"> The string representation of the GameState. </param>
    /// <returns> The corresponding GameStateType enum. </returns>
    public static Effects TransformStringToEffect(string effect) {
        switch (effect){
            case "BigJim_START":
                return Effects.BigJim;
            case "SlimJim_START":
                return Effects.SlimJim;
            case "Splitzy":
                return Effects.Splitzy;
            case "LifeUp":
                return Effects.LifeUp;
            case "LifeDown":
                return Effects.LifeDown;
            case "SpeedyGonzales":
                return Effects.SpeedyGonzales;
            case "BigBalls":
                return Effects.BigBalls;
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
            case Effects.SlimJim:
                return "SlimJim_START";
            case Effects.Splitzy:
                return "Splitzy";
            case Effects.LifeUp:
                return "LifeUp";
            case Effects.LifeDown:
                return "LifeDown";
            case Effects.SpeedyGonzales:
                return "SpeedyGonzales";
            case Effects.BigBalls:
                return "BigBalls";
            default:
                throw new NotImplementedException();
        }
    }

    public static string TransformEffectToPath(Effects effect) {
            switch (effect){
                case Effects.BigJim:
                    return "WidePowerUp.png";
                case Effects.SlimJim:
                    return "SlimJim.png";
                case Effects.Splitzy:
                    return "SplitPowerUp.png";
                case Effects.LifeUp:
                    return "LifePickUp.png";
                case Effects.LifeDown:
                    return "LoseLife.png";
                case Effects.SpeedyGonzales:
                    return "SpeedPickUp.png";
                case Effects.BigBalls:
                    return "BigPowerUp.png";
                default:
                    throw new NotImplementedException();
            }
        }

}
namespace Breakout.Effect;
public static class EffectTransformer {

    /// <summary> 
    /// Transforms a string representation of an effect into the corresponding effect type. 
    /// </summary>
    /// <param name="state"> The string representation of the effect. </param>
    /// <returns> The corresponding effect enum. </returns>
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

    /// <summary> Transforms an effect into the corresponding string representation of an effect. 
    /// </summary>
    /// <param name="state"> The effect type to be transformed into its string representation. 
    /// </param>
    /// <returns> The corresponding effect string representation. </returns>
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
 
    /// <summary> Given an effect enum returns the file path to the asset for that effect </summary>
    /// <param name="effect"> the effect enum. </param>
    /// <returns> The corresponding file path for the effect. </returns>
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
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Effect;
using Breakout.PlayerLives;
using DIKUArcade.Math;

namespace Breakout.BallClass;

public class Ball : Entity, IGameEventProcessor {
    bool isBig = false;

    public Ball(DynamicShape shape, IBaseImage image) : base (shape, image) {
        BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);
    }

    /// <summary>
    /// Checks if the ball is considered "dead" based on its position on the playable window.
    /// This method is mainly used for testing, and to assert the balls in BallContainers can die.
    /// </summary>
    /// <returns> Returns true if the ball is "dead" and otherwise returns false. </returns>
	public bool IsBallDead() {
        if (Shape.Position.Y <= 0.01f || Shape.Position.Y + Shape.Extent.Y <= 0.01f) {
            return true;
        } else {
            return false;
	    }
    }

    /// <summary>
    /// Creates a new ball and adds it to the ball container if no balls currently exist 
    /// and the player has remaining lives.
    /// </summary>
    /// <param name="ballContainer"> The container to which the new ball will be added. </param>
    /// <param name="levelLives"> The current lives the player has left. </param>
    public static void MakeNewBall(EntityContainer<Ball> ballContainer, Lives levelLives){
        if (ballContainer.CountEntities()==0 && levelLives.GetCurrentLives != 0) {
            levelLives.LoseLife();
            ballContainer.AddEntity(BallFactory.GenerateSemiRandomDirBall(new Vec2F(0.45f,0.30f)));
        }
    }

    /// <summary>
    /// Modifies the size of the Ball entity based on the given state.
    /// </summary>
    /// <param name="state"> The state indicating whether to start or stop the size modification. 
    /// </param>        		
    private void BigAffected(string state) {
        if ((state == "START") && (!isBig)) {
            isBig = true;

            Shape.Extent.X *= 2.0f;
            Shape.Extent.Y *= 2.0f;

        } else if (state == "STOP" && (isBig)) {
            isBig = false;

            Shape.Extent.X /= 2.0f;
            Shape.Extent.Y /= 2.0f;
        }
    }

    /// <summary>
    /// Initiates a specific effect based on the given effect and state parameters.
    /// </summary>
    /// <param name="effect"> The effect to be initiated. </param>
    /// <param name="state"> The state that indicates the effect's desired state. </param>
    public void InitiateEffect(string effect, string state) {
    // Method made public for testing access.
        switch (EffectTransformer.TransformStringToEffect(effect)) {
            case Effects.BigBalls:
                BigAffected(state);
            break;
        }

    }

    /// <summary> Processes a game event and updates the player's movement state. </summary>
    /// <param name="gameEvent"> The game event to process. </param>
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.PlayerEvent) {
            switch(gameEvent.Message) {
                case "EFFECT":
                    InitiateEffect(gameEvent.StringArg1, gameEvent.StringArg2);
                    break;
            }
        }
    }
}
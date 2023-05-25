using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;
using Breakout.Effect;

namespace Breakout.BallClass;

public class Ball : Entity, IGameEventProcessor {

    bool isBig = false;
    public Ball(DynamicShape shape, IBaseImage image) : base (shape, image) {
        BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);
    }
    public void Move() {
        if (Shape.Position.X > 0.0f && Shape.Position.X + Shape.Extent.X< 1.0f
                && Shape.Position.Y > 0.0f && Shape.Position.Y + Shape.Extent.Y< 1.0f ) {
            base.Shape.Move();
            }
        if (Shape.Position.X <= 0.01f || Shape.Position.X + Shape.Extent.X <= 0.01f || 
                Shape.Position.X >= 0.99f || Shape.Position.X + Shape.Extent.X >= 0.99f) {
            DirLR();
        }
        if (Shape.Position.Y >= 0.99f || Shape.Position.Y + Shape.Extent.Y >= 0.99f) {
            DirUD();
        }
    }

    // Called from GameRunning to handle Ball's new dirrection when colliding with Player.
    public void DirUp(Vec2F playerPos , Vec2F playerExtend) {
        var activeBall = this.Shape.AsDynamicShape();
        // Calculate players middle X possition.
        var playerMidX = playerPos + playerExtend / 2;

        // Calculates ball middle X possition.
        var ballMidX = activeBall.Position + activeBall.Extent / 2;
        
        // Calculates the different between the player middle and ball middle. 
        // Used to calculate the new vector for the balls dirrection.
        var ballPlayerDif = ballMidX - playerMidX;

        // Gets the balls speed so that it's magnitude will always stay constant.
        var ballSpeed = activeBall.Direction.Length();

        // Normalizes the vector and clamps it so if the ball collides with the player on the very 
        // left or right side, the ball won't shoot off almost sideways.
        var normalizedPos = Math.Clamp(Vec2F.Normalize(ballPlayerDif).X, -0.8f,0.8f);

        // "Draws" a half circle around the player to calculate the dirrection of the new Y vector,
        // Then times it with the vectors magnitude to have a constant speed. 
        var newDir = new Vec2F(normalizedPos, MathF.Sqrt(1.0f - normalizedPos * normalizedPos)) * (float)ballSpeed;
        ChangeDirection(newDir);
    }

    // Called from GameRunning to update ball direction when collision is detected Left and Right.
    public void DirLR() {
        var activeBall = this.Shape.AsDynamicShape();
        var newDirection = activeBall.Direction = new Vec2F(
                                activeBall.Direction.X*(-1),
                                activeBall.Direction.Y);
        ChangeDirection(newDirection);
    }

    // Called from GameRunning to update ball direction when collision is detected Up and Down.
    public void DirUD() {
        var activeBall = this.Shape.AsDynamicShape();
        var newDirection = activeBall.Direction = new Vec2F(
                            activeBall.Direction.X,
                            activeBall.Direction.Y*(-1));
        ChangeDirection(newDirection);
    }
    
    public void ChangeDirection(Vec2F newDir) {
        base.Shape.AsDynamicShape().ChangeDirection(newDir);
    }

	 public bool IsBallDead() {
        if (Shape.Position.Y <= 0.01f || Shape.Position.Y + Shape.Extent.Y <= 0.01f) {
            return true;
        } else {
            return false;
	}
					
   private void BigAffected(string state) {
        if ((state == "START") && (!isBig)) {
            isBig = true;

            Shape.Extent.X *= 2.0f;
            Shape.Extent.Y *= 2.0f;

        } else if (state == "STOP") {
            isBig = false;

            Shape.Extent.X /= 2.0f;
            Shape.Extent.Y /= 2.0f;
        }
    }

    private void initiateEffect(string effect, string state) {
        switch (EffectTransformer.TransformStringToEffect(effect)) {
            case Effects.BigBalls:
                BigAffected(state);
            break;
        }

    }

    /// <summary> Processes a game event and updates the player's movement state. </summary>
    /// <param name="gameEvent"> The game event to process. </param>
    /// <returns> Void. </returns> 
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.PlayerEvent) {
            switch(gameEvent.Message) {
                case "EFFECT":
                    initiateEffect(gameEvent.StringArg1, gameEvent.StringArg2);
                    break;
                
            }
        }
    }
}
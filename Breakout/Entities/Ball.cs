using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Effect;

namespace Breakout.BallClass;

public class Ball : Entity, IGameEventProcessor {

    bool isBig = false;
    public Ball(DynamicShape shape, IBaseImage image) : base (shape, image) {
        BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);
    }
    public void Move(Ball activeBall) {
        if (Shape.Position.X > 0.0f && Shape.Position.X + Shape.Extent.X< 1.0f
                && Shape.Position.Y > 0.0f && Shape.Position.Y + Shape.Extent.Y< 1.0f ) {
            base.Shape.Move();
            }
        if (Shape.Position.X <= 0.01f || Shape.Position.X + Shape.Extent.X <= 0.01f || 
                Shape.Position.X >= 0.99f || Shape.Position.X + Shape.Extent.X >= 0.99f) {
            BallMath.DirLR(activeBall);
        }
        if (Shape.Position.Y >= 0.99f || Shape.Position.Y + Shape.Extent.Y >= 0.99f) {
            BallMath.DirUD(activeBall);
        }
    }

    // Used primarry for testing of Ball, to assert the balls in BallContainer can die.
	 public bool IsBallDead() {
        if (Shape.Position.Y <= 0.01f || Shape.Position.Y + Shape.Extent.Y <= 0.01f) {
            return true;
        } else {
            return false;
	    }
    }
					
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

    private void InitiateEffect(string effect, string state) {
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
                    InitiateEffect(gameEvent.StringArg1, gameEvent.StringArg2);
                    break;
            }
        }
    }
}
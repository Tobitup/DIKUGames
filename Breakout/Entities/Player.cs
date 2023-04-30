using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;

namespace Breakout.Player;

public class Player : IGameEventProcessor {
    private GameEventBus eventBus;
    private Entity entity;
    private DynamicShape shape;
    public DynamicShape Shape {
        get {return shape;}
    }
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    const float MOVEMENT_SPEED = 0.02f;

    /// <summary> Creates a new Player object with the specified shape and image. </summary>
    /// <param name="shape"> The shape of the player's game object. </param>
    /// <param name="image"> The image used to render the player's game object. </param>
    /// <returns> Player object with a shape and image, along with an eventbus subsribed to
    ///           PlayerEvents </returns>
    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        this.shape = shape;
        eventBus = BreakoutBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, this);
    }

    /// <summary> Renders the player's game object. </summary>
     /// <returns> Void. </returns>
    public void Render() {
        entity.RenderEntity();
    }
    

    /// <summary> Sets the player's movement to the left. </summary>
    /// <param name="val"> Boolean value for starting or stopping player movement. </param>
    /// <returns> Void. </returns>
    private void SetMoveLeft(bool val) {
        if (val) {
            moveLeft -= MOVEMENT_SPEED;
        } else {
            moveLeft = 0f;
        }
        UpdateDirection(moveLeft);
    }

    /// <summary> Sets the player's movement to the right. </summary>
    /// <param name="val"> Boolean value for starting or stopping player movement. </param>
    /// <returns> Void. </returns>
    private void SetMoveRight(bool val) {
        if (val) {
            moveRight += MOVEMENT_SPEED;
        } else {
            moveRight = 0f;
        }
        UpdateDirection(moveRight);
    }

    /// <summary> Updates the direction of the player's shape. </summary>
    /// <param name="val"> The new X-axis direction value. </param>
    /// <returns> Void. </returns> 
    private void UpdateDirection(float val) {
        shape.Direction.X = val;
    }

    /// <summary> Moves the player's shape within the boundaries of the game screen. </summary>
    /// <returns> Void. </returns> 
    public void Move() {
        if (shape.Position.X > 0.0f && shape.Position.X + shape.Extent.X< 1.0f) {
            shape.Move();
        } else if (shape.Position.X < 0.0f && moveLeft == 0.0f) {
            shape.Move();
        } else if (shape.Position.X + shape.Extent.X > 1.0f && moveRight == 0.0f) {
            shape.Move();
        }
    }


    /// <summary> Processes a game event and updates the player's movement state. </summary>
    /// <param name="gameEvent"> The game event to process. </param>
    /// <returns> Void. </returns> 
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.PlayerEvent) {
            switch(gameEvent.Message) {
                case "MOVE_LEFT":
                    this.SetMoveLeft(true);
                    break;
                case "MOVE_RIGHT":
                    this.SetMoveRight(true);
                    break;
                
                // Key-Release Switch Cases
                case "MOVE_LEFT_STOP":
                    this.SetMoveLeft(false);
                    break;
                case "MOVE_RIGHT_STOP":
                    this.SetMoveRight(false);
                    break;
            }
        }
    }
}
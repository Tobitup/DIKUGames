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

    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        this.shape = shape;
        eventBus = BreakoutBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, this);
    }

    public void Render() {
        entity.RenderEntity();
    }
    

    private void SetMoveLeft(bool val) {
        if (val) {
            moveLeft -= MOVEMENT_SPEED;
        } else {
            moveLeft = 0f;
        }
        UpdateDirection(moveLeft);
    }

    private void SetMoveRight(bool val) {
        if (val) {
            moveRight += MOVEMENT_SPEED;
        } else {
            moveRight = 0f;
        }
        UpdateDirection(moveRight);
    }

    private void UpdateDirection(float val) {
        shape.Direction.X = val;  
    }

    public void Move() {
        if (shape.Position.X > 0.0f && shape.Position.X + shape.Extent.X< 1.0f) {
            shape.Move();
        } else if (shape.Position.X < 0.0f && moveLeft == 0.0f) {
            shape.Move();
        } else if (shape.Position.X + shape.Extent.X > 1.0f && moveRight == 0.0f) {
            shape.Move();
        }
    }


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
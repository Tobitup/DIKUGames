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
    public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
            eventBus = BreakoutBus.GetBus();
            eventBus.Subscribe(GameEventType.InputEvent, this);
        }
    public void Render() {
            entity.RenderEntity();
        }
    public void ProcessEvent(GameEvent gameEvent)
    {
        throw new NotImplementedException();
    }

    /* public void Move() {
        if (shape.Position.X > 0.0f && shape.Position.X + shape.Extent.X< 1.0f) {
            shape.Move();
        } else if (shape.Position.X < 0.0f && moveLeft == 0.0f) {
            shape.Move();
        } else if (shape.Position.X + shape.Extent.X > 1.0f && moveRight == 0.0f) {
            shape.Move();
        }
    }


    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.InputEvent) {
            switch(gameEvent.Message) {
                case "MOVE_LEFT":
                    this.SetMoveLeft(true);
                    break;
                case "MOVE_RIGHT":
                    this.SetMoveRight(true);
                    break;
                case "MOVE_UP":
                    this.SetMoveUp(true);
                    break;
                case "MOVE_DOWN":
                    this.SetMoveDown(true);
                    break;
                
                // Key-Release Switch Cases
                case "MOVE_LEFT_STOP":
                    this.SetMoveLeft(false);
                    break;
                case "MOVE_RIGHT_STOP":
                    this.SetMoveRight(false);
                    break;
                case "MOVE_UP_STOP":
                    this.SetMoveUp(false);
                    break;
                case "MOVE_DOWN_STOP":
                    this.SetMoveDown(false);
                    break;
            }
        }
    } */
}
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Effect;

namespace Breakout.Player;

public class Player : IGameEventProcessor {
    private GameEventBus eventBus;
    private Entity entity;
    private DynamicShape shape;
    public uint Lives = 0;
    private bool isSlimJimAffected = false;
    private bool isBigJimAffected = false;
    private bool isSpeedyAffected = false;

    public DynamicShape Shape {
        get {return shape;}
    }
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    const float MOVEMENT_SPEED = 0.02f;
    float MovementSpeedMultiplier = 1;

    public Entity GetEntity() => entity;

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
        Lives = 2;
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
            moveLeft -= MOVEMENT_SPEED*MovementSpeedMultiplier;
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
            moveRight += MOVEMENT_SPEED*MovementSpeedMultiplier;
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

    private void BigJimAffected(string state) {
        if ((state == "START") && (isBigJimAffected == false)) {
            isBigJimAffected = true;
            Vec2F bigJimSize = new Vec2F(Shape.Extent.X*2.0f, Shape.Extent.Y);

            float newX = bigJimSize.X/2.0f/2.0f;
            shape.Position.X -= newX;

            shape.Extent = bigJimSize;
        } else if (state == "STOP") {
            isBigJimAffected = false;
            Vec2F bigJimSize = new Vec2F(Shape.Extent.X/2.0f, Shape.Extent.Y);
            shape.Position.X += bigJimSize.X/2.0f;
            shape.Extent = bigJimSize;
        }
        
    }

    private void SlimJimAffected(string state) {
        if ((state == "START") && (!isSlimJimAffected)) {
            isSlimJimAffected = true;
            Vec2F slimJimSize = new Vec2F(Shape.Extent.X/2.0f, Shape.Extent.Y);
            shape.Position.X += slimJimSize.X/2.0f;

            shape.Extent = slimJimSize;
        } else if (state == "STOP") {
            isSlimJimAffected = false;
            Vec2F slimJimSize = new Vec2F(Shape.Extent.X*2.0f, Shape.Extent.Y);
            float newX = slimJimSize.X/2.0f/2.0f;

            shape.Position.X -= newX;

            shape.Extent = slimJimSize;
        }
    }

    private void SpeedyGonzalesAfected(string state) {
        if ((state == "START") && (!isSpeedyAffected)) {
            isSpeedyAffected = true;
            MovementSpeedMultiplier = 2.0f;

        } else if (state == "STOP") {
            isSpeedyAffected = false;
            MovementSpeedMultiplier = 1.0f;

        }
    }

    private void initiateEffect(string effect, string state) {
        switch (EffectTransformer.TransformStringToEffect(effect)) {
            case Effects.BigJim:
                BigJimAffected(state);
            break;
            case Effects.SlimJim:
                SlimJimAffected(state);
            break;
            case Effects.SpeedyGonzales:
                SpeedyGonzalesAfected(state);
            break;
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
                    
                case "EFFECT":
                    initiateEffect(gameEvent.StringArg1, gameEvent.StringArg2);
                    break;
                
            }
        }
    }
}
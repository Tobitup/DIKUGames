using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Galaga {
    public class Player : IGameEventProcessor {
        private GameEventBus eventBus;
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        private float moveUp = 0.0f;
        private float moveDown = 0.0f;
        const float MOVEMENT_SPEED = 0.01f;
        private Health health = new Health(new Vec2F(0.05f,0.4f), new Vec2F(0.3f,0.3f));
        public Health Health {
            get {return health;}
        }
        private enum axis {
            X,
            Y
        }

        private Entity entity;
        private DynamicShape shape;
        public DynamicShape Shape {
            get {return shape;}
        }
        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;

            eventBus = GalagaBus.GetBus();
            eventBus.Subscribe(GameEventType.InputEvent, this);
        }
        public void Render() {
            entity.RenderEntity();
            health.RenderHealth();
        }

        public Vec2F Extent() {
            return shape.Extent;
        }


        public void Move() {

            if (shape.Position.X > 0.0f && shape.Position.X + shape.Extent.X< 1.0f
            && shape.Position.Y > 0.0f && shape.Position.Y + shape.Extent.Y< 1.0f ) {
                shape.Move();
            } else if (shape.Position.X < 0.0f && moveLeft == 0.0f) {
                shape.Move();
            } else if (shape.Position.X + shape.Extent.X > 1.0f && moveRight == 0.0f) {
                shape.Move();
            } else if (shape.Position.Y + shape.Extent.Y > 1.0f && moveUp == 0.0f) {
                shape.Move();
            } else if (shape.Position.Y < 0.0f && moveDown == 0.0f) {
                shape.Move();
            }
        }
        private void SetMoveLeft(bool val) {

            if (val) {
                moveLeft -= MOVEMENT_SPEED;
            }
            else {
                moveLeft = 0f;
            }
            UpdateDirection(moveLeft, axis.X);
        }

        private void SetMoveRight(bool val) {

            if (val) {
                moveRight += MOVEMENT_SPEED;
            }
            else {
                moveRight = 0f;
            }
            UpdateDirection(moveRight, axis.X);
        }

        private void SetMoveUp(bool val) {

            if (val) {
                moveUp += MOVEMENT_SPEED;
            }
            else {
                moveUp = 0f;
            }
            UpdateDirection(moveUp, axis.Y);
        }
        private void SetMoveDown(bool val) {

            if (val) {
                moveDown -= MOVEMENT_SPEED;
            }
            else {
                moveDown = 0f;
            }
            UpdateDirection(moveDown, axis.Y);
        }

        public Vec2F GetPosition() {
            return shape.Position;
        }

        private void UpdateDirection(float val, axis dir) {
            if (dir == axis.X) {
                shape.Direction.X = val;
            } else if (dir == axis.Y) {
                shape.Direction.Y = val;
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
        }
    }
}
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Galaga {
    public class Player {
        // TODO: Add private fields
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        const float MOVEMENT_SPEED = 0.01f;


        private Entity entity;
        private DynamicShape shape;
        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
        }
        public void Render() {
            entity.RenderEntity();
        }


        public void Move() {
        // TODO: move the shape and guard against the window borders

            if (shape.Position.X > 0.0f && shape.Position.X + shape.Extent.X< 1.0f ) {
                shape.Move();
            } else if (shape.Position.X < 0.0f && moveRight != 0.0f) {
                shape.Move();
            } else if (shape.Position.X + shape.Extent.X > 1.0f && moveLeft != 0.0f) {
                shape.Move();
            }

        }
        public void SetMoveLeft(bool val) {
        // TODO:set moveLeft appropriately and call UpdateDirection()
            if (val) {
                moveLeft -= MOVEMENT_SPEED;
                UpdateDirection(moveLeft);
            }
            else {
                moveLeft = 0f;
                UpdateDirection(moveLeft);
            }
        
        }
        public void SetMoveRight(bool val) {
        // TODO:set moveRight appropriately and call UpdateDirection()
            if (val) {
                moveRight += MOVEMENT_SPEED;
                UpdateDirection(moveRight);
            }
            else {
                moveRight = 0f;
                UpdateDirection(moveRight);
            }
        }

        public Vec2F GetPosition() {
            return shape.Position;
        }

        private void UpdateDirection(float val) {
            shape.Direction.X = val;
        }
    }
}
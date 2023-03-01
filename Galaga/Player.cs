using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Galaga {
    public class Player {
        // TODO: Add private fields
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        private float moveUp = 0.0f;
        private float moveDown = 0.0f;
        const float MOVEMENT_SPEED = 0.01f;
        private enum axis {
            X,
            Y
        }

        private Entity entity;
        private DynamicShape shape;
        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
        }
        public void Render() {
            entity.RenderEntity();
        }

        public Vec2F Extent() {
            return shape.Extent;
        }


        public void Move() {
        // TODO: move the shape and guard against the window borders

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
        public void SetMoveLeft(bool val) {
        // TODO:set moveLeft appropriately and call UpdateDirection()
            if (val) {
                moveLeft -= MOVEMENT_SPEED;
                UpdateDirection(moveLeft, axis.X);
            }
            else {
                moveLeft = 0f;
                UpdateDirection(moveLeft, axis.X);
            }
        }

        public void SetMoveRight(bool val) {
        // TODO:set moveRight appropriately and call UpdateDirection()
            if (val) {
                moveRight += MOVEMENT_SPEED;
                UpdateDirection(moveRight, axis.X);
            }
            else {
                moveRight = 0f;
                UpdateDirection(moveRight, axis.X);
            }
        }

        public void SetMoveUp(bool val) {
        // TODO:set moveRight appropriately and call UpdateDirection()
            if (val) {
                moveUp += MOVEMENT_SPEED;
                UpdateDirection(moveUp, axis.Y);
            }
            else {
                moveUp = 0f;
                UpdateDirection(moveUp, axis.Y);
            }
        }
        public void SetMoveDown(bool val) {
        // TODO:set moveRight appropriately and call UpdateDirection()
            if (val) {
                moveDown -= MOVEMENT_SPEED;
                UpdateDirection(moveDown, axis.Y);
            }
            else {
                moveDown = 0f;
                UpdateDirection(moveDown, axis.Y);
            }
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
    }
}
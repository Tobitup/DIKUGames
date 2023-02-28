using DIKUArcade.Entities;
using DIKUArcade.Graphics;
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
            shape.Move();
        }
        public void SetMoveLeft(bool val) {
        // TODO:set moveLeft appropriately and call UpdateDirection()
            if (val) {
                moveLeft -= MOVEMENT_SPEED;
                UpdateDirection(moveLeft);
            }
        
        }
        public void SetMoveRight(bool val) {
        // TODO:set moveRight appropriately and call UpdateDirection()
            if (val) {
                moveRight += MOVEMENT_SPEED;
                UpdateDirection(moveRight);
            }
        }

        private void UpdateDirection(float val) {
            shape.Direction.X = val;
        }
    }
}

public class Player {
    
    
}
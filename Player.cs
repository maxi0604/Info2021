using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Info2021 {
    class Player : IUpdateable, ICollidable, IGetSetVelPos, IHasPosition {
        public Player() {
            Collider = new AttachedCollider(this, new Vector2(16f, 16f));
        }
        public VelPos VelPos { get; set; }
        public AttachedCollider Collider { get; }
        ICollider ICollidable.Collider { get => this.Collider; }
        public static readonly Vector2 Box = new Vector2(32, 32);
        public Vector2 Position => VelPos.P;
        
        public void Update(float dt) {
            // Gravity
            VelPos = VelPos.Accelerate(new Vector2(0, 9.81f));

            // Movement logic
            bool directionalMovement = true;
            if (InputManager.IsActive(InputEvent.Right))
                VelPos = VelPos.Accelerate(new Vector2(40f, 0));
            else if (InputManager.IsActive(InputEvent.Left))
                VelPos = VelPos.Accelerate(new Vector2(-40f, 0));
            else
                directionalMovement = false;
            
            // Air resistance
            if (directionalMovement)
                VelPos = VelPos.Accelerate(new Vector2(-0.05f * VelPos.V.X, 0));
            else
                VelPos = VelPos.Accelerate(new Vector2(-0.2f * VelPos.V.X, 0));
        }

        public void Accelerate(Vector2 a)
        {
            VelPos = VelPos.Accelerate(a);
        }
    }
}
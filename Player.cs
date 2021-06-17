using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Info2021 {
    class Player : IUpdateable, IHasPosition {
        VelPos velPos = new VelPos();
        public Collider col = new Collider(new Vector2(0,0), new Vector2(0,0));
        public static readonly Vector2 Box = new Vector2(32, 32);
        public Vector2 Position => velPos.P;

        public void Update(float dt) {
            col.LowerRight = Position - (Box / 2);
            col.UpperLeft = Position + (Box / 2);
            // Gravity
            velPos = velPos.Accelerate(new Vector2(0, 9.81f));

            // Movement logic
            bool directionalMovement = true;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                velPos = velPos.Accelerate(new Vector2(40f, 0));
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                velPos = velPos.Accelerate(new Vector2(-40f, 0));
            else
                directionalMovement = false;
            
            // Air resistance
            if (directionalMovement)
                velPos = velPos.Accelerate(new Vector2(-0.05f * velPos.V.X, 0));
            else
                velPos = velPos.Accelerate(new Vector2(-0.2f * velPos.V.X, 0));

            // Ground test. Remove this
            if (velPos.P.Y > 200) {
                velPos = velPos.Accelerate(new Vector2(0, -velPos.V.Y));
            }

            // Finalize physics by actually changing the position
            velPos = velPos.ApplyVelocity(dt);
        }

        
    }
}
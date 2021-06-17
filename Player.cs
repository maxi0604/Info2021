using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Info2021 {
    class Player : IUpdateable, IHasPosition {
        VelPos velPos = new VelPos();

        public Vector2 Position => velPos.P;
        public void Update(float dt) {

            velPos = velPos.Accelerate(new Vector2(0, 9.81f));
            bool directionalMovement = true;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                velPos = velPos.Accelerate(new Vector2(40f, 0));
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                velPos = velPos.Accelerate(new Vector2(-40f, 0));
            else
                directionalMovement = false;
            
            if (directionalMovement)
                velPos = velPos.Accelerate(new Vector2(-0.1f * velPos.V.X, 0));
            else
                velPos = velPos.Accelerate(new Vector2(-0.5f * velPos.V.X, 0));


            if (velPos.P.Y > 200) {
                velPos = velPos.Accelerate(new Vector2(0, -velPos.V.Y));
            }

            velPos = velPos.ApplyVelocity(dt);
        }
    }
}
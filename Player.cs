using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
namespace Info2021 {
    class Player : IUpdateable, IHasPosition {
        VelPos velPos = new VelPos();
        public Collider col = new Collider(new Vector2(0,0), new Vector2(0,0));
        public static readonly Vector2 Box = new Vector2(32, 32);
        public Vector2 Position => velPos.P;
        private Vector2 OldVelocity = new Vector2(0, 0);
        private bool fastFalling = false;
        private float timeSinceGround = 0;
        private float oldTimeSinceGround = 0;
        private float timeSinceJump = 1E10f;
        private float timeSinceInAirJumpPress = 1E10f;
        private float timeSinceNoJumpPress = 0;
        private bool jumping = false;
        const float gravity = 9.81f;
        public void Update(float dt) {
            col.LowerRight = Position - (Box / 2);
            col.UpperLeft = Position + (Box / 2);
            // Gravity
            
            velPos = velPos.Accelerate(new Vector2(0, (fastFalling ? 3 : 1) * gravity));

            // Movement logic
            bool directionalMovement = true;
            if (InputManager.IsActive(InputEvent.Right) && !fastFalling)
                velPos = velPos.Accelerate(new Vector2(40f, 0));
            else if (InputManager.IsActive(InputEvent.Left) && !fastFalling)
                velPos = velPos.Accelerate(new Vector2(-40f, 0));
            else
                directionalMovement = false;
            
            if(InputManager.IsActive(InputEvent.Down) && !fastFalling) {
                StartFalling();
            }
            if(!InputManager.IsActive(InputEvent.Down) && fastFalling) {
                StopFalling();
            }
            if(oldTimeSinceGround > 0 && timeSinceGround == 0) {
                OnInitialGroundCollision();
            }
            if (velPos.P.Y > 224) {
                velPos = velPos.Accelerate(new Vector2(0, -velPos.V.Y));
                velPos = new VelPos(velPos.V, new Vector2(Position.X, 224));
                OnGroundCollision();
            }
            // Air resistance
            if (directionalMovement)
                velPos = velPos.Accelerate(new Vector2(-0.05f * velPos.V.X, 0));
            else
                velPos = velPos.Accelerate(new Vector2(-0.2f * velPos.V.X, 0));

            // Vertical air resistance
            if (velPos.V.Y > 300 && !fastFalling)
                velPos = velPos.Accelerate(new Vector2(0, -0.05f * velPos.V.Y));
            else if(velPos.V.Y > 400)
                velPos = velPos.Accelerate(new Vector2(0, -0.02f * velPos.V.Y));
            // Ground test. Remove this
            if(InputManager.IsActive(InputEvent.Jump) && !OnGround() ) {
                timeSinceInAirJumpPress = 0;
            }
            if(!InputManager.IsActive(InputEvent.Jump)) {
                timeSinceNoJumpPress = 0;
            }
            if((timeSinceNoJumpPress < 0.15 && OnGround() && oldTimeSinceGround >= timeSinceNoJumpPress && (timeSinceInAirJumpPress < 0.15 || InputManager.IsActive(InputEvent.Jump))) ||
            (InputManager.IsActive(InputEvent.Jump) && jumping)) {
                Jump();
            }
            // Finalize physics by actually changing the position
            velPos = velPos.ApplyVelocity(dt);
            if(Position.Y > 1000) velPos = new VelPos(velPos.V, new Vector2(Position.X, 10));
            timeSinceGround += dt;
            timeSinceJump += dt;
            timeSinceInAirJumpPress += dt;
            timeSinceNoJumpPress += dt;
            if(timeSinceGround > 0.1f && timeSinceJump > 0.14f) {
                jumping = false;
            }
            Console.WriteLine(Position.X);
        }
        public void StartFalling() {
            if(OnGround()) return;
            fastFalling = true;
            OldVelocity = velPos.V;
            velPos = new VelPos(new Vector2(0, velPos.V.Y + 50), velPos.P);
        }
        public void StopFalling() {
            fastFalling = false;
            velPos = new VelPos(OldVelocity, velPos.P);
            if(OnGround())
            velPos = new VelPos(new Vector2(0, OldVelocity.Y), velPos.P);
        }
        public bool OnGround() {
            return velPos.P.Y >= 224;
        }

        public void OnInitialGroundCollision() {
            StopFalling();
        }

        public void OnGroundCollision() {
            oldTimeSinceGround = timeSinceGround;
            timeSinceGround = 0;
            
        }

        public void Jump() {
            
            if(!jumping) {
                velPos = velPos.Accelerate(new Vector2(0, -50));
            }
            jumping = true;
            timeSinceJump = timeSinceGround;
            float accel = - (1 - timeSinceJump * 7) * 50;
            if(accel > 0) {
                jumping = false;
                return;
            }
            velPos = velPos.Accelerate(new Vector2(0, accel - gravity));
            


        }
    }
}
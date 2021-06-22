using Info2021.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace Info2021 {
     class Player : DynamicObject, IUpdateable, IAttachedColliderParent, Interfaces.IDrawable {
        // Position data
        public VelPos VelPos { get; set; }
        public override Vector2 Position => VelPos.P;
        // Collision
        readonly Vector2 hitbox = new Vector2(14, 16);
        public AttachedCollider Collider { get; }
        // Jump-related data
        private Vector2 OldVelocity = new Vector2(0, 0);
        private bool fastFalling = false;
        private float timeSinceGround = 0;
        private float oldTimeSinceGround = 0;
        private float timeSinceJump = 1E10f;
        private float timeSinceInAirJumpPress = 1E10f;
        private float timeSinceNoJumpPress = 0;
        private bool jumping = false;
        // Gravity
        const float gravity = 9.81f;

        bool isAlive = true;

        public Player(Vector2 position) {
            Collider = new AttachedCollider(this, hitbox);
            VelPos = new VelPos(Vector2.Zero, position);
        }
        public override void Update(float dt) {
            // Gravity
            timeSinceGround += dt;
            timeSinceJump += dt;
            timeSinceInAirJumpPress += dt;
            timeSinceNoJumpPress += dt;
            VelPos = VelPos.Accelerate(new Vector2(0, (fastFalling ? 3 : 1) * gravity));

            // Movement logic
            bool directionalMovement = true;
            if (InputManager.IsActive(InputEvent.Right))
                VelPos = VelPos.Accelerate(new Vector2(40f, 0));
            else if (InputManager.IsActive(InputEvent.Left))
                VelPos = VelPos.Accelerate(new Vector2(-40f, 0));
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
         
            // Air resistance
            if (directionalMovement)
                VelPos = VelPos.Accelerate(new Vector2(-0.1f * VelPos.V.X, 0));
            else
                VelPos = VelPos.Accelerate(new Vector2(-0.2f * VelPos.V.X, 0));

            // Vertical air resistance
            if (VelPos.V.Y > 300 && !fastFalling)
                VelPos = VelPos.Accelerate(new Vector2(0, -0.05f * VelPos.V.Y));
            else if(VelPos.V.Y > 400)
                VelPos = VelPos.Accelerate(new Vector2(0, -0.02f * VelPos.V.Y));

            
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
            VelPos = VelPos.ApplyVelocity(1/60f);
                        
            if(timeSinceGround > 0.1f && timeSinceJump > 0.14f) {
                jumping = false;
            }
            
        }
        public void StartFalling() {
            if(OnGround()) return;
            fastFalling = true;
            OldVelocity = VelPos.V;
            VelPos = new VelPos(new Vector2(0, VelPos.V.Y + 50), VelPos.P);
        }
        public void StopFalling() {
            fastFalling = false;
            VelPos = new VelPos(OldVelocity, VelPos.P);
            if(OnGround())
            VelPos = new VelPos(new Vector2(0, OldVelocity.Y), VelPos.P);
        }
        public bool OnGround() {
            return timeSinceGround < (1f/50f);
        }

        public void OnInitialGroundCollision() {
            StopFalling();
        }

        public void OnGroundCollision() {
            oldTimeSinceGround = timeSinceGround;
            timeSinceGround = 0;
        }

        void IAttachedColliderParent.OnCollision(float alongX, float alongY, Vector2 accelVel)
        {
            if(accelVel.Y < 0) 
                OnGroundCollision();
        }
        public void Jump() {
            
            if(!jumping) {
                VelPos = VelPos.Accelerate(new Vector2(0, -50));
            }
            jumping = true;
            timeSinceJump = timeSinceGround;
            float accel = - (1 - timeSinceJump * 7) * 50;
            if(accel > 0) {
                jumping = false;
                return;
            }
            VelPos = VelPos.Accelerate(new Vector2(0, accel - gravity));
            


        }

        public override Texture2D GetTexture(ResourceAccessor resourceAccessor)
        {
            // TODO: Animation depending on state
            return resourceAccessor.LoadContent<Texture2D>("Character");
        }

        public bool IsAlive() {
            // might need change for special cases
            return isAlive;
        }

        public void Die() {
            isAlive = false;
        }
    }
}
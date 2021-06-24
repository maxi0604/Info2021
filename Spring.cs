using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Info2021
{
    class Spring : CinematicObject
    {
        public override Vector2 Position { get; }
        private bool hasTouchedLastFrame, hasTouchedThisFrame;
        private Vector2 direction;
        // 0 = face left, 1 = face upwards, etc
        int rotation;

        public Spring(Vector2 position, int rotation)
        {
            Position = position;
            this.rotation = rotation;
            hasTouchedLastFrame = false;
            hasTouchedThisFrame = false;
            Vector2 topLeft;
            Vector2 diag;
            switch(rotation) {
                case 0:
                    topLeft = position + Vector2.UnitX * 8;
                    diag = new Vector2(8, 16);
                    direction = -Vector2.UnitX;
                    break;
                case 1:
                    topLeft = position;
                    diag = new Vector2(16, 8);
                    direction = Vector2.UnitY;
                    break;
                case 2:
                    topLeft = position;
                    diag = new Vector2(8, 16);
                    direction = Vector2.UnitX;
                    break;
                case 3:
                    topLeft = position + Vector2.UnitY * 8;
                    diag = new Vector2(16,8);
                    direction = -Vector2.UnitY;
                    break;
                default:
                    throw new System.InvalidOperationException();
            }
            CCollider = new CinematicCollider(this, topLeft, diag);
        }

        public override Texture2D GetTexture(ResourceAccessor resourceAccessor)
        {
            
            return resourceAccessor.GetSprite(7, 15 - rotation);
        }

        public override void OnCollision(Player player)
        {
            float newVel = 0;
            if(player.OnGround())
                // increase position by a bit to avoid ground issues
                player.VelPos = player.VelPos.Translate(direction * 3);
            
            // don't bounce two times at once
            if(hasTouchedLastFrame) return;


            if(rotation % 2 == 0)
                // if you're moving against the spring, reverse that velocity, otherwise gain some
                if(Math.Sign(player.VelPos.V.X) == -Math.Sign(direction.X) && !player.OnGround())
                    newVel = - player.VelPos.V.X;
                player.VelPos = player.VelPos.WithVelocity(new Vector2(MathF.MaxMagnitude(direction.X * 400, newVel), player.VelPos.V.Y));
            if(rotation % 2 == 1) {
                if(Math.Sign(player.VelPos.V.Y) == -Math.Sign(direction.Y) && !player.OnGround())
                    newVel = - player.VelPos.V.Y;
                player.VelPos = player.VelPos.WithVelocity(new Vector2(player.VelPos.V.X, MathF.MaxMagnitude(direction.Y * 400, newVel)));
            }
            
            hasTouchedThisFrame = true;
            }

        public override void Update(float dt, Player player)
        {
            hasTouchedLastFrame = hasTouchedThisFrame;
            hasTouchedThisFrame = false;
        }
    }
}

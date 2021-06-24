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
                player.VelPos = new VelPos(player.VelPos.V, player.VelPos.P + direction * 3);
            
            if(hasTouchedLastFrame) return;
            if(rotation % 2 == 0)
                if(Math.Sign(player.VelPos.V.X) == -Math.Sign(direction.X) && !player.OnGround())
                    newVel = - player.VelPos.V.X;
                player.VelPos = new VelPos(new Vector2(direction.X * 200 + newVel, player.VelPos.V.Y), player.VelPos.P);
            if(rotation % 2 == 1) {
                if(Math.Sign(player.VelPos.V.Y) == -Math.Sign(direction.Y) && !player.OnGround())
                    newVel = - player.VelPos.V.Y;
                player.VelPos = new VelPos(new Vector2(player.VelPos.V.X, direction.Y * 200 + newVel), player.VelPos.P);
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

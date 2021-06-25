using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Info2021
{
    class MovingPlatform : CinematicObject, ICinematicColliderParent
    {
        public VelPos VelPos { get; set; }
        public StaticCollider Collider;
        public override Vector2 Position => VelPos.P;
        CinematicCollider cinematicCollider;
        CinematicCollider ICinematicColliderParent.CCollider => cinematicCollider;

        Vector2 translationOnNextFrame = Vector2.Zero;

        public float MaxTime;
        private float movingTime;


        public MovingPlatform(Vector2 position, Vector2 velocity, float maxTime)
        {
            VelPos = new VelPos(velocity, position);
            Collider = new StaticCollider(VelPos.P, VelPos.P + Vector2.One * 16);
            cinematicCollider = new CinematicCollider(this, VelPos.P - Vector2.UnitY * 2, new Vector2(16, 8));
            
            MaxTime = maxTime;
            movingTime = 0;
        }

        public override Texture2D GetTexture(ResourceAccessor resourceAccessor)
        {
            return resourceAccessor.GetSprite(0,13);
        }

        public override void Update(float dt, Player player)
        {
            player.VelPos = player.VelPos.Translate(translationOnNextFrame);
            translationOnNextFrame = Vector2.Zero;

            if(movingTime > MaxTime)
            {
                movingTime = 0;
                VelPos = VelPos.WithVelocity(-VelPos.V);
            }
            VelPos = VelPos.ApplyVelocity(dt);
            Collider.TopLeft = Position;
            Collider.BottomRight = Position + Vector2.One * 16;
            cinematicCollider.TopLeft = VelPos.P - Vector2.UnitY * 2;
            CCollider = cinematicCollider;
            movingTime += dt;
        }

        public override void OnCollision(Player player)
        {
            // move player with the platform
            translationOnNextFrame = VelPos.V/60;
            
        }
    }
}
using Info2021.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Info2021
{
    class MovingPlatform : CinematicObject, Interfaces.ICinematicColliderParent
    {
        public VelPos VelPos { get; set; }
        public StaticCollider Collider;
        public override Vector2 Position => VelPos.P;
        CinematicCollider cinematicCollider;
        CinematicCollider ICinematicColliderParent.CCollider => cinematicCollider;


        public float MaxTime;
        private float movingTime;


        public MovingPlatform(Vector2 position, Vector2 velocity, float maxTime)
        {
            VelPos = new VelPos(velocity, position);
            Collider = new StaticCollider(VelPos.P, VelPos.P + Vector2.One * 16);
            cinematicCollider = new CinematicCollider(this, position - Vector2.One * 2, Vector2.One * 20);
            
            MaxTime = maxTime;
            movingTime = 0;
        }

        public override Texture2D GetTexture(ResourceAccessor resourceAccessor)
        {
            return resourceAccessor.GetSprite(0,12);
        }

        public override void Update(float dt, Player player)
        {
            if(movingTime > MaxTime)
            {
                movingTime = 0;
                VelPos = new VelPos(-VelPos.V, VelPos.P);
            }
            VelPos = VelPos.ApplyVelocity(dt);
            Collider.TopLeft = Position;
            Collider.BottomRight = Position + Vector2.One * 16;
            cinematicCollider.TopLeft = Position;
            CCollider = cinematicCollider;
            movingTime += dt;
        }

        public override void OnCollision(Player player)
        {
            //if(player.Position.Y < Position.Y)
                player.VelPos = player.VelPos.Accelerate(VelPos.V * 10);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Info2021
{
    class Goal : CinematicObject
    {
        public Goal(Vector2 position)
        {
            Position = position;
            CCollider = new CinematicCollider(this, position, Vector2.One * 16);
        }

        public override Vector2 Position { get; }
        public override Texture2D GetTexture(ResourceAccessor resourceAccessor)
        {
            return resourceAccessor.GetSprite(4,5);
        }

        public override void OnCollision(Player player)
        {
            player.BeatLevel();
        }

        public override void Update(float dt, Player player)
        {
        }
    }
}
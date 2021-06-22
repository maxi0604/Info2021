using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Info2021
{
    class Spikes : CinematicObject
    {
        public override Vector2 Position { get; }
        // 0 = face left, 1 = face upwards, etc
        int rotation;

        public Spikes(Vector2 position, int rotation)
        {
            Position = position;
            this.rotation = rotation;
            Vector2 topLeft;
            Vector2 diag;
            switch(rotation) {
                case 0:
                    topLeft = position + Vector2.UnitX * 12;
                    diag = new Vector2(4, 16);
                    break;
                case 1:
                    topLeft = position;
                    diag = new Vector2(16, 4);
                    break;
                case 2:
                    topLeft = position;
                    diag = new Vector2(4, 16);
                    break;
                case 3:
                    topLeft = position + Vector2.UnitY * 12;
                    diag = new Vector2(16,4);
                    break;
                default:
                    throw new System.InvalidOperationException();
            }
            Collider = new CinematicCollider(this, topLeft, diag);
        }

        public override Texture2D GetTexture(ResourceAccessor resourceAccessor)
        {
            return resourceAccessor.LoadContent<Texture2D>("spikes" + rotation.ToString());
        }

        public override void OnCollision(Player player)
        {
            player.Die();
        }

        public override void Update(float dt)
        { }
    }
}
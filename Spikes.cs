// Gehört zu Johannes

using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Info2021 {
    [DataContract]
    class Spikes : CinematicObject, ILevelElement {
        [DataMember]
        // position of 16 * 16 tile it is inside of (overridden from DynamicObject)
        public override Vector2 Position { get; set; }
        [DataMember]
        //actual topLeft of hitbox
        private Vector2 topLeft;
        [DataMember]
        private Vector2 diag;
        // 0 = face left, 1 = face downwards, etc
        [DataMember]
        int rotation;

        public Spikes(Vector2 position, int rotation) {
            Position = position;
            this.rotation = rotation;


            // spikes are always "inside" the tile
            switch (rotation) {
                // face left
                case 0:
                    topLeft = position + Vector2.UnitX * 14;
                    diag = new Vector2(2, 16);
                    break;
                // face down
                case 1:
                    topLeft = position;
                    diag = new Vector2(16, 2);
                    break;
                // face right
                case 2:
                    topLeft = position;
                    diag = new Vector2(2, 16);
                    break;
                // face up
                case 3:
                    topLeft = position + Vector2.UnitY * 14;
                    diag = new Vector2(16, 2);
                    break;
                default:
                    throw new System.InvalidOperationException();
            }
            // create new CinematicCollider with previously defined parameters
            CCollider = new CinematicCollider(this, topLeft, diag);
        }

        public override Texture2D GetTexture(ResourceAccessor resourceAccessor) {

            return resourceAccessor.GetSprite(13, 15 - rotation);
        }
        // overriden from cinematic object
        public override void OnCollision(Player player) {
            player.Die();
        }

        public override void Update(float dt, Player player) {
            CCollider = new CinematicCollider(this, topLeft, diag);
        }
    }
}
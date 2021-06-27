using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Info2021 {
    [DataContract]
    class Spikes : CinematicObject, ILevelElement {
        [DataMember]
        public override Vector2 Position { get; set; }
        [DataMember]
        private Vector2 topLeft;
        [DataMember]
        private Vector2 diag;
        // 0 = face left, 1 = face upwards, etc
        [DataMember]
        int rotation;

        public Spikes(Vector2 position, int rotation) {
            Position = position;
            this.rotation = rotation;


            // spikes are always "inside" the tile
            switch (rotation) {
                case 0:
                    topLeft = position + Vector2.UnitX * 12;
                    diag = new Vector2(2, 16);
                    break;
                case 1:
                    topLeft = position;
                    diag = new Vector2(16, 2);
                    break;
                case 2:
                    topLeft = position;
                    diag = new Vector2(2, 16);
                    break;
                case 3:
                    topLeft = position + Vector2.UnitY * 12;
                    diag = new Vector2(16, 2);
                    break;
                default:
                    throw new System.InvalidOperationException();
            }
            CCollider = new CinematicCollider(this, topLeft, diag);
        }

        public override Texture2D GetTexture(ResourceAccessor resourceAccessor) {

            return resourceAccessor.GetSprite(13, 15 - rotation);
        }

        public override void OnCollision(Player player) {
            player.Die();
        }

        public override void Update(float dt, Player player) {
            CCollider = new CinematicCollider(this, topLeft, diag);
        }
    }
}
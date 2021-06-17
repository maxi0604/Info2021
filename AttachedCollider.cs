using Microsoft.Xna.Framework;
namespace Info2021
{
    // A collider attached to an object.
    class AttachedCollider : ICollider {
        private IHasPosition parent;
        private Vector2 tl_to_br;
        public Vector2 TopLeft { get => parent.Position; }
        public Vector2 BottomRight { get => parent.Position + tl_to_br; }
        public AttachedCollider(IHasPosition p, Vector2 tlbr) {
            parent = p;
            tl_to_br = tlbr;
        }

        public Vector2 CollideWith(ICollider other) {
            if (this.TopLeft.X <= other.TopLeft.X && this.TopLeft.Y <= other.TopLeft.Y
                && this.BottomRight.X >= other.TopLeft.X && this.BottomRight.Y >= other.TopLeft.Y) {

                return  this.BottomRight - other.TopLeft;
            }
            return new Vector2();
        }
    }
}
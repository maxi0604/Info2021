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

        public Vector2? CollideWith(ICollider other) {
            if (this.TopLeft.X <= other.BottomRight.X && this.BottomRight.X >= other.TopLeft.X &&
                this.TopLeft.Y <= other.BottomRight.Y && this.BottomRight.Y >= other.TopLeft.Y) {
                
                float xExit = this.BottomRight.X - other.TopLeft.X;
                float yExit = this.BottomRight.Y - other.TopLeft.Y;
                if (xExit < yExit)
                    return new Vector2(-1, 1);
                else
                    return new Vector2(1, -1);
            }
            return null;
        }
    }
}
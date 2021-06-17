using Microsoft.Xna.Framework;

namespace Info2021 {
    class StaticCollider : ICollider, ICollidable {
        public Vector2 TopLeft { get; }
        public Vector2 BottomRight { get; }

        public ICollider Collider => this;

        public VelPos VelPos { get => new VelPos(new Vector2(), this.TopLeft); set {} }

        public StaticCollider(Vector2 tl, Vector2 br) {
            TopLeft = tl;
            BottomRight = br;
        }

        public Vector2 CollideWith(ICollider other) => new Vector2();
    }
}
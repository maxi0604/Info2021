using System;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace Info2021
{
    [DataContract]
    class StaticCollider : ILevelElement {
        public StaticCollider(Vector2 topLeft, Vector2 bottomRight)
        {
            if (topLeft.X > bottomRight.X || topLeft.Y > bottomRight.Y)
                throw new ArgumentOutOfRangeException("Top left vector has to actually be to the left and above bottom right vector.");

            TopLeft = topLeft;
            BottomRight = bottomRight;
        }
        [DataMember]
        public Vector2 TopLeft { get; set; }
        [DataMember]
        public Vector2 BottomRight { get; set; }
        public Vector2 Center { get => (TopLeft + BottomRight) / 2; }

        public void Add(Level level) {
            level.staticColliders.Add(this);
        }
    }
}
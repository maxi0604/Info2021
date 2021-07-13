// Gehört zu Maxi

using System;
using static System.MathF;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization;

namespace Info2021 {
    [DataContract]
    class CinematicCollider {
        [DataMember]

        public CinematicObject Parent { get; set; }
        [DataMember]
        public Vector2 TopLeft;
        [DataMember]
        Vector2 diagonal;
        public Vector2 BottomRight { get => TopLeft + diagonal; }
        public Vector2 Center { get => TopLeft + diagonal / 2; }

        public CinematicCollider(CinematicObject parent, Vector2 topLeft, Vector2 diagonal) {
            if (diagonal.X < 0 || diagonal.Y < 0)
                throw new ArgumentOutOfRangeException("Diagonal vector has to be pointing from the top left to the bottom right.");

            this.diagonal = diagonal;
            this.TopLeft = topLeft;
            this.Parent = parent;
        }
        public void CollideWith(Player player) {
            // check whether hitboxes of player and objekt are inside of each outher
            if (BottomRight.X > player.Collider.TopLeft.X && TopLeft.X < player.Collider.BottomRight.X
                && BottomRight.Y > player.Collider.TopLeft.Y && TopLeft.Y < player.Collider.BottomRight.Y)
                Parent.OnCollision(player);

        }
    }
}
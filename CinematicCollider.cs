using System;
using static System.MathF;
using Microsoft.Xna.Framework;
namespace Info2021
{
    class CinematicCollider
    {
        public ICinematicColliderParent Parent { get; }
        public Vector2 TopLeft;
        Vector2 diagonal;
        public Vector2 BottomRight { get => TopLeft + diagonal; }
        public Vector2 Center { get => TopLeft + diagonal / 2; }

        public CinematicCollider(ICinematicColliderParent parent, Vector2 topLeft, Vector2 diagonal) {
            if (diagonal.X < 0 || diagonal.Y < 0)
                throw new ArgumentOutOfRangeException("Diagonal vector has to be pointing from the top left to the bottom right.");
            
            this.diagonal = diagonal;
            this.TopLeft = topLeft;
            this.Parent = parent;
        }
        public void CollideWith(Player player)
        {
            if (BottomRight.X > player.Collider.TopLeft.X && TopLeft.X < player.Collider.BottomRight.X
                && BottomRight.Y > player.Collider.TopLeft.Y && TopLeft.Y < player.Collider.BottomRight.Y)
                Parent.OnCollision(player);

        }
    }
}
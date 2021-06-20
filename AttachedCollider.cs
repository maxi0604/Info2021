using System;
using static System.MathF;
using Microsoft.Xna.Framework;

namespace Info2021
{
    class AttachedCollider {
        public IAttachedColliderParent Parent { get; }
        public Vector2 TopLeft { get => Parent.VelPos.P; }
        Vector2 diagonal;
        public Vector2 BottomRight { get => TopLeft + diagonal; }
        public Vector2 Center { get => TopLeft + diagonal / 2; }

        public AttachedCollider(IAttachedColliderParent parent, Vector2 diagonal) {
            if (diagonal.X < 0 || diagonal.Y < 0)
                throw new ArgumentOutOfRangeException("Diagonal vector has to be pointing from the top left to the bottom right.");
            
            this.diagonal = diagonal;
            this.Parent = parent;
        }

        // https://spicyyoghurt.com/tutorials/html5-javascript-game-development/collision-detection-physics
        // https://www.metanetsoftware.com/2016/n-tutorial-a-collision-detection-and-response#section1
        // https://stackoverflow.com/questions/46172953/aabb-collision-resolution-slipping-sides
        public bool CollideWith(StaticCollider other) {
            if (BottomRight.X <= other.TopLeft.X || TopLeft.X >= other.BottomRight.X
                || BottomRight.Y <= other.TopLeft.Y || TopLeft.Y >= other.BottomRight.Y)
            {
                // No collision took place.
                return false;
            }

            // How far we need to travel along the respective axes to uncollide.
            float alongX = this.Center.X > other.Center.X ? other.BottomRight.X - TopLeft.X : other.TopLeft.X - BottomRight.X;
            float alongY = this.Center.Y > other.Center.Y ? other.BottomRight.Y - TopLeft.Y : other.TopLeft.Y - BottomRight.Y;

            // We resolve along the shortest axis.
            bool resolveAlongX = Abs(alongX) < Abs(alongY);
            bool aboveOther = this.Center.Y < other.Center.Y;
            bool leftOfOther = this.Center.Y < other.Center.Y;

            Vector2 oldVel = Parent.VelPos.V;
            Vector2 accelVel = Vector2.Zero;

            if (resolveAlongX) {
                // If we are already moving in the correct direction, don't negate current velocity...
                if (leftOfOther && oldVel.X > 0 || !leftOfOther && oldVel.X < 0)
                    // Otherwise do it...
                    accelVel = new Vector2(-oldVel.X, 0);
            }
            else {
                // Same as above.
                if (aboveOther && oldVel.Y > 0 || !aboveOther && oldVel.Y < 0)
                    accelVel = new Vector2(0, -oldVel.Y);
            }
            
            Parent.OnCollision(alongX, alongY, accelVel);
            Parent.VelPos = Parent.VelPos.Accelerate(accelVel);
            if (BottomRight.X > other.TopLeft.X && TopLeft.X < other.BottomRight.X
                && BottomRight.Y > other.TopLeft.Y && TopLeft.Y < other.BottomRight.Y)
            {
                // A collision took place and was resolved.
                return true;
            }
            CollideWith(other);
            return false;
        }
    }
}

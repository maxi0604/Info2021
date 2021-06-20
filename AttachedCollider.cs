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
        public bool CollideWith(StaticCollider other)
        {
            if (BottomRight.X <= other.TopLeft.X || TopLeft.X >= other.BottomRight.X
                || BottomRight.Y <= other.TopLeft.Y || TopLeft.Y >= other.BottomRight.Y)
            {
                // No collision took place.
                return false;
            }

            Vector2 collisionVec = other.Center - this.Center;
            // The general relative velocity is other.V - this.V.
            // However, we know that we are colliding with a static collider, so the other
            // velocity is by definition 0.
            Vector2 relativeV = Parent.VelPos.V;

            float alongX = this.Center.X > other.Center.X ? other.BottomRight.X - TopLeft.X : other.TopLeft.X - BottomRight.X;
            float alongY = this.Center.Y > other.Center.Y ? other.BottomRight.Y - TopLeft.Y : other.TopLeft.Y - BottomRight.Y;
            bool resolveAlongX = Abs(alongX) < Abs(alongY);
            bool aboveOther = this.Center.Y < other.Center.Y;
            bool leftOfOther = this.Center.Y < other.Center.Y;

            Vector2 oldVel = Parent.VelPos.V;
            Vector2 accelVel = Vector2.Zero;

            if (resolveAlongX)
            {
                if (leftOfOther && oldVel.X > 0 || !leftOfOther && oldVel.X < 0)
                    accelVel = new Vector2(-oldVel.X, 0);
            }
            else
            {
                if (aboveOther && oldVel.Y > 0 || !aboveOther && oldVel.Y < 0)
                    accelVel = new Vector2(0, -oldVel.Y);
            }

            // The collision is going to resolve itself anyway since the objects are separating.

            // We want to uncollide, so we move opposite of the collision direction.
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
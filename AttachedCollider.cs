using System;
using static System.MathF;
using Microsoft.Xna.Framework;

namespace Info2021
{
    public class AttachedCollider {
        public IAttachedColliderParent Parent { get; }
        public Vector2 TopLeft { get => Parent.VelPos.P + offset; }
        Vector2 diagonal;
        Vector2 offset;
        public Vector2 BottomRight { get => TopLeft + diagonal; }
        public Vector2 Center { get => TopLeft + diagonal / 2; }

        public AttachedCollider(IAttachedColliderParent parent, Vector2 diagonal) : this(parent, diagonal, Vector2.Zero) {}
        public AttachedCollider(IAttachedColliderParent parent, Vector2 diagonal, Vector2 offset) {
            if (diagonal.X < 0 || diagonal.Y < 0)
                throw new ArgumentOutOfRangeException("Diagonal vector has to be pointing from the top left to the bottom right.");
            
            this.diagonal = diagonal;
            this.offset = offset;
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

            Vector2 oldVel = Parent.VelPos.V;
            Vector2 accelVel;

            // Set in which direction we need to go.
            if (resolveAlongX) {
                accelVel = new Vector2(-oldVel.X, 0);
            }
            else {
                accelVel = new Vector2(0, -oldVel.Y);
            }
            
            // Only resolve the collision if we aren't moving outside of the object already anyway
            // i. e. the resolution velocity doesn't point in the same direction as the current velocity.
            if (Vector2.Dot(oldVel, accelVel) < 0) {
                Parent.VelPos = Parent.VelPos.Accelerate(accelVel);

                // Push this collider out of the other one.
                if (resolveAlongX)
                    Parent.VelPos = Parent.VelPos.Translate(new Vector2(alongX, 0));
                else
                    Parent.VelPos = Parent.VelPos.Translate(new Vector2(0, alongY));
            }

            Parent.OnCollision(alongX, alongY, accelVel);

            return true;
        }
    }
}

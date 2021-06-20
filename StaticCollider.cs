using System;
using Microsoft.Xna.Framework;

namespace Info2021
{
    class StaticCollider {
        public StaticCollider(Vector2 topLeft, Vector2 bottomRight)
        {
            Console.WriteLine("we do a little trolling");
            if (topLeft.X > bottomRight.X || topLeft.Y > bottomRight.Y)
                throw new ArgumentOutOfRangeException("Top left vector has to actually be to the left and above bottom right vector.");

            TopLeft = topLeft;
            BottomRight = bottomRight;
        }

        public Vector2 TopLeft { get; }
        public Vector2 BottomRight { get; }
        public Vector2 Center { get => (TopLeft + BottomRight) / 2; }
    }
}
using System;
using Microsoft.Xna.Framework;

namespace Info2021 {
    class Collider : IHasPosition {
        public Vector2 UpperLeft;
        public Vector2 LowerRight;
        public Vector2 Position => (UpperLeft + LowerRight) / 2;

        public Collider(Vector2 upper, Vector2 lower) {
            UpperLeft = upper;
            LowerRight = lower;
        }
    }
}
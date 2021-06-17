using System;
using Microsoft.Xna.Framework;

namespace Info2021 {
    class Collider : IHasPosition {
        public Vector2 Lower;
        public Vector2 Upper;
        public Vector2 Position => (Lower + Upper) / 2;

        public Collider(Vector2 lower, Vector2 upper) {
            Lower = lower;
            Upper = upper;
        }
    }
}
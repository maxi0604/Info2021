using System;
using Microsoft.Xna.Framework;

namespace Info2021 {
    readonly struct VelPos {
        public Vector2 V { get; }
        public Vector2 P { get; }
        public VelPos(Vector2 v, Vector2 p) {
            V = v;
            P = p;
        }

<<<<<<< HEAD
        public VelPos ApplyVelocity(float t) {
            return new VelPos(V, P + t * V);
        }
=======
        public VelPos ApplyVelocity (float t) => this.Translate(V * t);
>>>>>>> bfd8407c6f75f7f88bcd165e4046d4949b6d5150

        public VelPos Translate(Vector2 tr, float factor = 1) => new VelPos(V, P + factor * tr);

        public VelPos Accelerate(Vector2 a, float factor = 1) => new VelPos(V + factor * a, P);
    }
}
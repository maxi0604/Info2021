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

        public VelPos ApplyVelocity (float t) {
            return new VelPos(V, P + t * V);
        }

        public VelPos Translate(Vector2 tr) {
            return new VelPos(V, P + tr);
        }

        public VelPos Accelerate(Vector2 a) {
            return new VelPos(V + a, P);
        }
    }
}
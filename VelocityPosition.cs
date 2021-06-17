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

        public VelPos ApplyVelocity (float t) => this.Translate(V * t);

        public VelPos Translate(Vector2 tr, float factor = 1) => new VelPos(V, P + factor * tr);

        public VelPos Accelerate(Vector2 a, float factor = 1) => new VelPos(V + factor * a, P);
    }
}
using System;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace Info2021 {
    [DataContract]
    public struct VelPos {
        [DataMember]
        public Vector2 V { get; set; }
        [DataMember]
        public Vector2 P { get; set; }
        public VelPos(Vector2 v, Vector2 p) {
            V = v;
            P = p;
        }

        public VelPos ApplyVelocity(float t) => this.Translate(V * t);

        public VelPos Translate(Vector2 tr, float factor = 1) => new VelPos(V, P + factor * tr);

        public VelPos Accelerate(Vector2 a, float factor = 1) => new VelPos(V + factor * a, P);
        public VelPos WithPosition(Vector2 p) => new VelPos(V, p);
        public VelPos WithVelocity(Vector2 v) => new VelPos(v, P);
    }
}
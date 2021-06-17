using System;
using Microsoft.Xna.Framework;

namespace Info2021 {
    interface ICollider {
        Vector2 TopLeft { get; }
        Vector2 BottomRight { get; }
        Vector2? CollideWith(ICollider other);
    }
}
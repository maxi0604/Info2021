// Gehört zu Maxi

using Microsoft.Xna.Framework;
namespace Info2021 {
    interface IAttachedColliderParent {
        VelPos VelPos { get; set; }
        AttachedCollider Collider { get; }

        void OnCollision(float alongX, float alongY, Vector2 accelVel);
    }
}
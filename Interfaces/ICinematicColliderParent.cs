using System.Runtime.Serialization;

namespace Info2021
{
    interface ICinematicColliderParent
    {
        void OnCollision(Player player);
        CinematicCollider CCollider {get;}
    }
}
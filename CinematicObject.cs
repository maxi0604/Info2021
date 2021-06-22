using Info2021.Interfaces;
namespace Info2021
{
    abstract class CinematicObject : DynamicObject, ICinematicColliderParent
    {
        public CinematicCollider Collider { get; set; }

        public abstract void OnCollision(Player player);
    }
}
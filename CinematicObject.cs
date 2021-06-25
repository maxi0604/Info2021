namespace Info2021
{
    abstract class CinematicObject : DynamicObject, ICinematicColliderParent
    {
        public CinematicCollider CCollider { get; set; }

        public abstract void OnCollision(Player player);
    }
}
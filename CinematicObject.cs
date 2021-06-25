namespace Info2021
{
    abstract class CinematicObject : DynamicObject, ICinematicColliderParent, ILevelElement
    {
        public CinematicCollider CCollider { get; set; }

        public abstract void OnCollision(Player player);

        public override void AddHelper(Level level) {
            level.cinematicObjects.Add(this);
        }
    }
}
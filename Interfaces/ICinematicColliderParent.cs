namespace Info2021.Interfaces
{
    public interface ICinematicColliderParent
    {
        void OnCollision(Player player);
        CinematicCollider Collider {get;}
    }
}
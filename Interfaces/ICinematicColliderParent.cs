namespace Info2021.Interfaces
{
    interface ICinematicColliderParent
    {
        void OnCollision(Player player);
        CinematicCollider CCollider {get;}
    }
}
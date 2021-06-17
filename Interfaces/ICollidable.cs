namespace Info2021
{
    interface ICollidable  {
        ICollider Collider { get; }
        VelPos VelPos { get; set; }
    }
}
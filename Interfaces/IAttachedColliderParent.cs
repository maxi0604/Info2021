namespace Info2021
{
    interface IAttachedColliderParent {
        VelPos VelPos { get; set; }
        AttachedCollider Collider { get; }
    }
}
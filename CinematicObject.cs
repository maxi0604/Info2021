using System.Runtime.Serialization;

namespace Info2021 {
    [DataContract(IsReference = true)]
    abstract class CinematicObject : DynamicObject, ICinematicColliderParent, ILevelElement {
        [DataMember]
        public CinematicCollider CCollider { get; set; }

        public abstract void OnCollision(Player player);

        public override void AddHelper(Level level) {
            level.cinematicObjects.Add(this);
        }
    }
}
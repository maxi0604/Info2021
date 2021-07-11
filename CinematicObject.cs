using System.Runtime.Serialization;

namespace Info2021 {
    [DataContract(IsReference = true)]
    abstract class CinematicObject : DynamicObject, ILevelElement {
        [DataMember]
        // is valid in contrast to "abstract", but can be overriden by inheriting classes
        virtual public CinematicCollider CCollider { get; set; }
        // has to be overriden by inheriting classes
        public abstract void OnCollision(Player player);

        public override void AddHelper(Level level) {
            level.cinematicObjects.Add(this);
        }
    }
}
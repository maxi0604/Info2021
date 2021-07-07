using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Info2021 {
    [DataContract(IsReference = true)]
    abstract class DynamicObject : IHasPosition, IDrawable, ILevelElement {
        [DataMember]
        public abstract Vector2 Position { get; set; }
        public abstract Texture2D GetTexture(ResourceAccessor resourceAccessor);
        public abstract void Update(float dt, Player player);

        // dynamic objects get drawn at their current position
        public void Draw(IRenderer renderer, ResourceAccessor accessor, Vector2 camPos) {
            renderer.Draw(camPos, GetTexture(accessor), Position, 0, Vector2.Zero, 1, 0.1f);
        }

        // Needed because interface implementations cannot necessarily be overridden
        public virtual void AddHelper(Level level) {
            level.dynamicObjects.Add(this);
        }
        public virtual void Add(Level level) {

            AddHelper(level);
        }
    }
}
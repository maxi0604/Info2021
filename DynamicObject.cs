using Info2021.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Info2021
{
    abstract class DynamicObject : IHasPosition, Interfaces.IDrawable, IUpdateable
    {
        public abstract Vector2 Position { get; }
        public abstract Texture2D GetTexture(ResourceAccessor resourceAccessor);
        public abstract void Update(float dt);
        public void Draw(IRenderer renderer, ResourceAccessor accessor, Vector2 camPos)
        {
            renderer.Draw(camPos, GetTexture(accessor), Position, 0, Vector2.Zero, 1, 0.1f);
        }
    }
}
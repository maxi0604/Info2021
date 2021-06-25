using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Info2021
{
    class Background : IDrawable
    {
        public Texture2D texture;

        public Background(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Draw(IRenderer renderer, ResourceAccessor accessor, Vector2 camPos)
        {
            renderer.Draw(camPos, texture, camPos, 0, Vector2.Zero, 1, 0.99f);
        }

        
    }
}
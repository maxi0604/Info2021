using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Info2021.Interfaces;

namespace Info2021
{
    public class Background : Interfaces.IDrawable
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
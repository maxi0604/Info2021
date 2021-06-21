using Microsoft.Xna.Framework.Graphics;
namespace Info2021
{
    public abstract class Renderer
    {
        protected Renderer(SpriteBatch sprite) {
            spriteBatch = sprite;
        }
        protected SpriteBatch spriteBatch;
    }
}
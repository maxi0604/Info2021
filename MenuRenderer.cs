using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Info2021
{
    class MenuRenderer : Renderer, Interfaces.IRenderer
    {
        public MenuRenderer(SpriteBatch sprite) : base(sprite) {
            
        }
        public void Draw(Vector2 cameraPosition, Texture2D texture, Vector2 position, float rotation, Vector2 origin, float scale, float layerDepth)
        {
            spriteBatch.Draw(texture, position * 2, null, Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0.001f);
        }
    }
}
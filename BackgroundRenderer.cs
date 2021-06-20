using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Info2021
{
    public class BackgroundRenderer : Interfaces.IRenderer
    {
        private SpriteBatch spriteBatch;

        public BackgroundRenderer(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }
        public void Draw(Vector2 cameraPosition, Texture2D texture, Vector2 position, float rotation, Vector2 origin, float scale, float layerDepth)
        {
            float x = ((cameraPosition.X/2) % 1280) + (cameraPosition.X*2);
            float x2 = x + 1280;
            float x3 = x - 1280;
            spriteBatch.Draw(texture, new Vector2(x, cameraPosition.Y), null, Color.White, 0, cameraPosition, 2, SpriteEffects.None, layerDepth);
            spriteBatch.Draw(texture, new Vector2(x2, cameraPosition.Y), null, Color.White, 0, cameraPosition, 2, SpriteEffects.None, layerDepth+0.001f);
            spriteBatch.Draw(texture, new Vector2(x3, cameraPosition.Y), null, Color.White, 0, cameraPosition, 2, SpriteEffects.None, layerDepth+0.002f);
        }
    }
}
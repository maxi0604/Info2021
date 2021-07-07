using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Info2021 {
    class BackgroundRenderer : Renderer, IRenderer {
        
        public BackgroundRenderer(SpriteBatch sprite) : base(sprite) {

        }
        public void Draw(Vector2 cameraPosition, Texture2D texture, Vector2 position, float rotation, Vector2 origin, float scale, float layerDepth) {
            float x = ((cameraPosition.X / 2) % 1280) + (cameraPosition.X * 2);
            float x2 = x + 1280;
            float x3 = x - 1280;
            // draw three offset copies to make sure to fill the screen
            spriteBatch.Draw(texture, new Vector2(x, cameraPosition.Y * 2), null, Color.White, 0, cameraPosition, 2, SpriteEffects.None, layerDepth);
            spriteBatch.Draw(texture, new Vector2(x2, cameraPosition.Y * 2), null, Color.White, 0, cameraPosition, 2, SpriteEffects.None, layerDepth + 0.001f);
            spriteBatch.Draw(texture, new Vector2(x3, cameraPosition.Y * 2), null, Color.White, 0, cameraPosition, 2, SpriteEffects.None, layerDepth + 0.002f);
        }
    }
}
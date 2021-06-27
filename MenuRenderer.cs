using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Info2021 {
    class MenuRenderer : Renderer, IRenderer {
        public MenuRenderer(SpriteBatch sprite) : base(sprite) {

        }
        public void Draw(Vector2 cameraPosition, Texture2D texture, Vector2 position, float rotation, Vector2 origin, float scale, float layerDepth) {
            spriteBatch.Draw(texture, position * 2, null, Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0.2f);
        }

        public void DrawText(Vector2 position, SpriteFont font, string text, Color color) {
            spriteBatch.DrawString(font, text, position * 2, color, 0, Vector2.Zero, 2, SpriteEffects.None, 0.11f);
        }
    }
}
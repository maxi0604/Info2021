// Gehört zu Arvid

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Info2021 {
    class TileRenderer : Renderer, IRenderer {
        public TileRenderer(SpriteBatch sprite) : base(sprite) {

        }

        public void Draw(Vector2 cameraPosition, Texture2D texture, Vector2 position, float rotation, Vector2 origin, float scale, float layerDepth) {
            int x, y;
            (x, y) = CamTranslator.Translator(cameraPosition, position);
            // only draw tile if it is visible
            if (x > -20 && x < 1320 && y > -20 && y < 760) {
                spriteBatch.Draw(texture, new Vector2(x, y), null, Color.White, rotation, origin, 2 * scale, SpriteEffects.None, layerDepth);
            }
        }
    }
}
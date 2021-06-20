using Info2021.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Info2021
{
    public class TileRenderer : IRenderer
    {
        private SpriteBatch spriteBatch;

        public TileRenderer(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public void Draw(Vector2 cameraPosition, Texture2D texture, Vector2 position, float rotation, Vector2 origin, float scale, float layerDepth)
        {
            int x, y;
            (x, y) = CamTranslator.Translator(cameraPosition, position);
            if(x > -20 && x < 1320 && y > -20 && y < 760) {
                spriteBatch.Draw(texture, new Vector2(x, y), null, Color.White, rotation, origin, 2 * scale, SpriteEffects.None, layerDepth);
            }
        }

        public void Begin() { 
            spriteBatch.Begin();
        }

        public void End() {
            spriteBatch.End();
        }
    }
}
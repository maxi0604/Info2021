using Info2021.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Info2021
{
    public class PlayerRenderer : Renderer, IRenderer
    {
        public PlayerRenderer(SpriteBatch sprite) : base(sprite) {
            
        }
        public void Draw(Vector2 cameraPosition, Texture2D texture, Vector2 position, float rotation, Vector2 origin, float scale, float layerDepth)
        {
            int x, y;
            (x, y) = CamTranslator.Translator(cameraPosition, position);
            spriteBatch.Draw(texture, new Vector2(x, y), null, Color.White, rotation, origin, 2 * scale, SpriteEffects.None, layerDepth);
        }
    }
}
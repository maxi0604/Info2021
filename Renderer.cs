// Gehört zu Arvid

using Microsoft.Xna.Framework.Graphics;
namespace Info2021 {
    abstract class Renderer {
        protected Renderer(SpriteBatch sprite) {
            spriteBatch = sprite;
        }
        protected SpriteBatch spriteBatch;
    }
}
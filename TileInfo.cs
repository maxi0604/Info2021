using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Info2021
{
    public record TileInfo(Texture2D Texture, float layer, int width, int height)
    {
        public TileInfo(Game1 game, string texturePath, float layer, int width, int height) : 
            this(game.Content.Load<Texture2D>(texturePath), layer, width, height)
            { }
        public TileInfo(Game1 game, string texturePath, float layer) : 
            this(game.Content.Load<Texture2D>(texturePath), layer, 0, 0) {
                height = Texture.Height;
                width = Texture.Width;
        }
        public TileInfo(Game1 game, string texturePath, int width, int height) :
            this(game.Content.Load<Texture2D>(texturePath), 0.1f, width, height) { }
        public TileInfo(Game1 game, string texturePath) : 
            this(game.Content.Load<Texture2D>(texturePath), 0.1f, 0, 0) {
                height = Texture.Height;
                width = Texture.Width;
        }
    }
}
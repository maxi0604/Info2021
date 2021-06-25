using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Info2021
{
    record TileInfo(Texture2D Texture, float layer, int width, int height)
    {
        public TileInfo(ResourceAccessor resourceAccessor, string texturePath, float layer, int width, int height) : 
            this(resourceAccessor.LoadContent<Texture2D>(texturePath), layer, width, height)
            { }
        public TileInfo(ResourceAccessor resourceAccessor, string texturePath, float layer) : 
            this(resourceAccessor.LoadContent<Texture2D>(texturePath), layer, 0, 0) {
                height = Texture.Height;
                width = Texture.Width;
        }
        public TileInfo(ResourceAccessor resourceAccessor, string texturePath, int width, int height) :
            this(resourceAccessor.LoadContent<Texture2D>(texturePath), 0.1f, width, height) { }
        public TileInfo(ResourceAccessor resourceAccessor, string texturePath) : 
            this(resourceAccessor.LoadContent<Texture2D>(texturePath), 0.1f, 0, 0) {
                height = Texture.Height;
                width = Texture.Width;
        }
        public TileInfo(ResourceAccessor resourceAccessor, int x, int y) : this(resourceAccessor.GetSprite(x,y), 0.1f, 16, 16) {

        }
    }
}
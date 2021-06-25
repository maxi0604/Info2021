using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization;

namespace Info2021
{
    [DataContract]
    class TileInfo
    {
        [DataMember]
        public float layer;
        [DataMember]
        public int width, height, tx, ty;
        /*
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
        */ 
        public TileInfo(int x, int y) : this(0.1f, 16, 16, x, y) {

        }

        public TileInfo(float layer, int width, int height, int tx, int ty)
        {
            this.layer = layer;
            this.width = width;
            this.height = height;
            this.tx = tx;
            this.ty = ty;
        }
    }
}
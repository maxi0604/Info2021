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
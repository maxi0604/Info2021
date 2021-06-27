using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
namespace Info2021 {
    [DataContract]
    class Tile : IDrawable, ILevelElement {
        [DataMember]
        public TileInfo Info { get; set; }
        [DataMember]
        public (int, int) TilePos;
        public Vector2 Position => new Vector2(TilePos.Item1 * Info.width, TilePos.Item2 * Info.height);

        public Tile(TileInfo info, int posX, int posY) {
            Info = info;
            TilePos = (posX, posY);
        }

        public void Draw(IRenderer renderer, ResourceAccessor accessor, Vector2 camPos) {
            renderer.Draw(camPos, accessor.GetSprite(Info.tx, Info.ty), Position, 0, Vector2.Zero, 1, Info.layer);
        }

        public void Add(Level level) {
            level.tiles.Add(this);
        }
    }
}
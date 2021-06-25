using Microsoft.Xna.Framework;
namespace Info2021
{
    class Tile : IDrawable
    {
        public TileInfo Info  { get; }
        private (int, int) tilePos;
        public Vector2 Position => new Vector2(tilePos.Item1 * Info.width, tilePos.Item2 * Info.height);

        public Tile(TileInfo info, int posX, int posY) {
            Info = info;
            tilePos = (posX, posY);
        }

        public void Draw(IRenderer renderer, ResourceAccessor accessor, Vector2 camPos)
        {
            renderer.Draw(camPos, Info.Texture, Position, 0, Vector2.Zero, 1, Info.layer);
        }
    }
}
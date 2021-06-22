using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Info2021
{
    class ResourceAccessor
    {
        private Game game;
        public Texture2D[] SpriteSheet;
        private int xCount, yCount;
        public ResourceAccessor(Game game_, Texture2D[] spritesheet, int xc, int yc) {
            game = game_;
            this.SpriteSheet = spritesheet;
            xCount = xc;
            yCount = yc;
        }

        public A LoadContent<A>(string name) {
            return game.Content.Load<A>(name); 
        }

        public Texture2D GetSprite(int x, int y) {
            return SpriteSheet[y * xCount + x];
        }
    }
}
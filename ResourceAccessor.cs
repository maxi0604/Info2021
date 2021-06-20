using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Info2021
{
    public class ResourceAccessor
    {
        private Game game;
        public ResourceAccessor(Game game_) {
            game = game_;
        }

        public A LoadContent<A>(string name) {
            return game.Content.Load<A>(name); 
        }
    }
}
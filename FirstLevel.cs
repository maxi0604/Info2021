using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Info2021
{
    static class FirstLevel
    {
        public static Level GetLevel(Game1 game) {
            List<Tile> tiles = new List<Tile>();
            List<StaticCollider> staticColliders = new List<StaticCollider>();
            for(int i = 0; i < 30; i++) {
                tiles.Add(new Tile(new TileInfo(game, "Character"), i, 15));
                
            }
             for(int i = 0; i < 30; i++) {
                tiles.Add(new Tile(new TileInfo(game, "Character"), 30, i));
                
            }
            for(int i = 12; i < 15; i++) {
                Tile tile;
                StaticCollider staticCollider;
                (tile, staticCollider) = GroundHelper(new TileInfo(game, "Character"), 12, i);
                tiles.Add(tile);
                staticColliders.Add(staticCollider);
            }
            Tile tile1;
            StaticCollider staticCollider1;
            (tile1, staticCollider1) = GroundHelper(new TileInfo(game, "Character"), 14, 11);
            staticColliders.Add(staticCollider1);
            tiles.Add(tile1);
            staticColliders.Add(new StaticCollider(new Vector2(0, 15*16), new Vector2(450, 16*16)));
            staticColliders.Add(new StaticCollider(new Vector2(30*16, 0), new Vector2(31*16, 450)));

            return new Level(Vector2.Zero, Vector2.Zero, tiles, staticColliders, new List<DynamicObject>(),
            new List<CinematicObject>() {
                new Spikes(new Vector2(5*16,14*16), 3),
                new Spikes(new Vector2(6*16,14*16), 3),
                new Spikes(new Vector2(5*16,13*16), 1),
                new Spikes(new Vector2(6*16,13*16), 1),
                new Goal(new Vector2(9*16, 10*16)),
                new Spring(new Vector2(20*16, 12*16), 0),
                new Spring(new Vector2(19*16, 14*16), 3)
                },
                new Background(game.resourceAccessor.LoadContent<Texture2D>("background1")));
        }

        public static (Tile, StaticCollider) GroundHelper(TileInfo info, int x, int y) {
            return (new Tile(info, x, y), new StaticCollider(new Vector2(16*x, 16*y), new Vector2(16*x+15, 16*y+15)));
        }
    }
}

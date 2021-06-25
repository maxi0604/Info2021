using System.Collections.Generic;
using System.IO;
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
                tiles.Add(new Tile(new TileInfo(2, 0), i, 15));
                
            }
             for(int i = 0; i < 30; i++) {
                tiles.Add(new Tile(new TileInfo(0, 2), 30, i));
                
            }
            
            Tile tile1;
            MovingPlatform platform = new MovingPlatform(new Vector2(22*16, 12*16), new Vector2(-4*16,0), 3);
            StaticCollider staticCollider1;
            
            
            staticColliders.Add(new StaticCollider(new Vector2(0, 15*16), new Vector2(450, 16*16)));
            staticColliders.Add(new StaticCollider(new Vector2(30*16, 0), new Vector2(31*16, 450)));
            
            //
            //tiles = BinarySerializer.Deserialize<List<Tile>>(File.OpenRead("Czfhfdsh"));
            Level a = new Level(Vector2.Zero, Vector2.Zero, tiles, staticColliders, new List<DynamicObject>(),
            new List<CinematicObject>() {
                new Spikes(new Vector2(5*16,14*16), 3),
                new Spikes(new Vector2(6*16,14*16), 3),
                new Spikes(new Vector2(5*16,13*16), 1),
                new Spikes(new Vector2(6*16,13*16), 1),
                new Goal(new Vector2(9*16, 10*16)),
                new Spring(new Vector2(20*16,12*16), 0),
                new Spring(new Vector2(19*16, 14*16), 3),                
                },
                new Background("background1"));
            platform.Add(a);
            a.AddSolidTile(new TileInfo(17, 0), 14, 11);
            // when the loading is funktioniering!!!!!1!!!!
            for(int i = 12; i < 15; i++) {
                a.AddSolidTile(new TileInfo(17, 0), 12, i);
            }
            
            return Level.Load("testlevel");
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework;
namespace Info2021 {
    [DataContract]
    [KnownType(typeof(Spikes))]
    [KnownType(typeof(Goal))]
    [KnownType(typeof(MovingPlatform))]
    [KnownType(typeof(Spring))]
    class Level {
        public Level(Vector2 spawnPosition, Vector2 camPos, List<Tile> tiles, List<StaticCollider> staticColliders,
                List<DynamicObject> dynamicObjects, List<CinematicObject> cinematicObjects, Background background) {
            this.spawnPosition = spawnPosition;
            this.cameraPosition = camPos;
            this.tiles = tiles;
            this.staticColliders = staticColliders;
            this.dynamicObjects = dynamicObjects;
            this.cinematicObjects = cinematicObjects;
            this.background = background;
        }
        [DataMember]
        public Vector2 spawnPosition { get; set; }
        [DataMember]
        public Vector2 cameraPosition { get; set; }
        [DataMember]
        public List<Tile> tiles { get; set; }
        [DataMember]
        public List<StaticCollider> staticColliders { get; set; }
        [DataMember]
        public List<DynamicObject> dynamicObjects { get; set; }
        [DataMember]
        public List<CinematicObject> cinematicObjects { get; set; }
        [DataMember]
        public Background background { get; set; }

        public static Level Load(string path) {
            Level a = BinarySerializer.Deserialize<Level>(File.OpenRead(path));

            // create new level to add everything to in order to allow for initialization
            Level b = new Level(Vector2.Zero, Vector2.Zero, new List<Tile>(), new List<StaticCollider>(),
            new List<DynamicObject>(), new List<CinematicObject>(), new Background("background1"));
            foreach (var t in a.cinematicObjects) t.Add(b);
            foreach (var t in a.dynamicObjects) t.Add(b);
            foreach (var t in a.staticColliders) t.Add(b);
            foreach (var t in a.tiles) t.Add(b);
            b.spawnPosition = a.spawnPosition;
            b.cameraPosition = a.cameraPosition;
            return b;
        }

        public void Save(string path) {
            using (Stream file = File.Create(path))
                BinarySerializer.Serialize<Level>(this, file);
        }
        public void AddSolidTile(TileInfo info, int x, int y) {
            new Tile(info, x, y).Add(this);
            new StaticCollider(new Vector2(16 * x, 16 * y), new Vector2(16 * x + 15, 16 * y + 15)).Add(this);
        }
    }
}
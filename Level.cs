using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework;
namespace Info2021
{
    [Serializable]
    class Level
    {
        public Level(Vector2 spawnPosition, Vector2 camPos, List<Tile> tiles, List<StaticCollider> staticColliders,
                List<DynamicObject> dynamicObjects, List<CinematicObject> cinematicObjects, Background background)
        {
            this.spawnPosition = spawnPosition;
            this.camPos = camPos;
            this.tiles = tiles;
            this.staticColliders = staticColliders;
            this.dynamicObjects = dynamicObjects;
            this.cinematicObjects = cinematicObjects;
            this.background = background;
        }

        public Vector2 spawnPosition { get; set; }
        public Vector2 camPos { get; set; }
        public List<Tile> tiles { get;  set; }
        public List<StaticCollider> staticColliders { get; set; }
        public List<DynamicObject> dynamicObjects { get; set; }
        public List<CinematicObject> cinematicObjects { get; set; }
        public Background background { get; set; }

        

    }
}
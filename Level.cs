using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace Info2021
{
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

        public Vector2 spawnPosition { get; }
        public Vector2 camPos { get; }
        public List<Tile> tiles { get; }
        public List<StaticCollider> staticColliders { get; }
        public List<DynamicObject> dynamicObjects { get; }
        public List<CinematicObject> cinematicObjects { get; }
        public Background background { get; }

        

    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework;
namespace Info2021
{
    [DataContract]
    [KnownType(typeof(Spikes))]
    [KnownType(typeof(Goal))]
    [KnownType(typeof(MovingPlatform))]
    [KnownType(typeof(Spring))]
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
        [DataMember]
        public Vector2 spawnPosition { get; set; }
        [DataMember]
        public Vector2 camPos { get; set; }
        [DataMember]
        public List<Tile> tiles { get;  set; }
        [DataMember]
        public List<StaticCollider> staticColliders { get; set; }
        [DataMember]
        public List<DynamicObject> dynamicObjects { get; set; }
        [DataMember]
        public List<CinematicObject> cinematicObjects { get; set; }
        [DataMember]
        public Background background { get; set; }

        

    }
}
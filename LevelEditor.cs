using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Info2021
{
    class LevelEditor
    {
        ResourceAccessor resourceAccessor;
        SpriteBatch spriteBatch;
        Vector2 camPos = Vector2.Zero;
        TileRenderer tileRenderer;
        BackgroundRenderer backgroundRenderer;
        DynamicRenderer dynamicRenderer;

        int[] indices;
        int indexIndex;
        Level level;
        Background background;

        Vector2 Position = Vector2.Zero;

        public LevelAddables currentAddables;
        private static HashSet<InputEvent> haveBecomeActive = new HashSet<InputEvent>();

        private Tile GetTile(int count) {
            return new Tile(new TileInfo(count % 16, count / 16),
                (int) Position.X/16, (int) Position.Y/16);
        }

        private CinematicObject GetCinematic(int count) {
            CinematicObject[] allCinems =
            {new Goal(Position),
            new Spikes(Position, 0),
            new Spikes(Position, 1),
            new Spikes(Position, 2),
            new Spikes(Position, 3),
            new Spring(Position, 0),
            new Spring(Position, 1),
            new Spring(Position, 2),
            new Spring(Position, 3),
            new MovingPlatform(Position, 64 * Vector2.UnitX, 3),
            new MovingPlatform(Position, 64 * Vector2.UnitY, 3),
            new MovingPlatform(Position, -64 * Vector2.UnitX, 3),
            new MovingPlatform(Position, -64 * Vector2.UnitY, 3)};
            return allCinems[count % allCinems.Length];
        }

        public LevelEditor(ResourceAccessor resourceAccessor, SpriteBatch spriteBatch) {
            this.resourceAccessor = resourceAccessor;
            this.spriteBatch = spriteBatch;
            tileRenderer = new TileRenderer(spriteBatch);
            backgroundRenderer = new BackgroundRenderer(spriteBatch);
            dynamicRenderer = new DynamicRenderer(spriteBatch);
            background = new Background("background1");
            level = new Level(Vector2.Zero, Vector2.Zero, new List<Tile>(),
                new List<StaticCollider>(), new List<DynamicObject>(), new List<CinematicObject>(),
                background);
            indices = new int[] {0,0,0,0};
        }
        public void Initialize() {
            if(level.dynamicObjects.Count > 0 && level.dynamicObjects[0] is Player) {
                level.dynamicObjects.RemoveAt(0);
            }
            
        }
        public void Update(float gameTime) {
            indexIndex = (int) currentAddables;

            // change screen
            if(Position.X - camPos.X > 640) {
                
                camPos.X += 640;
            }
            if(Position.X - camPos.X < -16) {
                
                camPos.X -= 640;
            }
            if(Position.Y - camPos.Y > 376) {
                
                camPos.Y += 360;
            }
            if(Position.Y - camPos.Y < -16) {
                
                camPos.Y -= 360;
            }


            HashSet<InputEvent> oldActive = haveBecomeActive;
            haveBecomeActive = new HashSet<InputEvent>();
            foreach (var item in (InputEvent[])Enum.GetValues(typeof(InputEvent)))
            {
                if((InputManager.IsActive(item) && !oldActive.Contains(item))) {
                    haveBecomeActive.Add(item);
                }
            }

            if(haveBecomeActive.Contains(InputEvent.Right)) 
                Position.X += 16;
            if(haveBecomeActive.Contains(InputEvent.Left)) 
                Position.X -= 16;
            if(haveBecomeActive.Contains(InputEvent.Up)) 
                Position.Y -= 16;
            if(haveBecomeActive.Contains(InputEvent.Down)) 
                Position.Y += 16;
            if(haveBecomeActive.Contains(InputEvent.Jump)) {
                bool isThere = false;
                foreach(var x in level.tiles){
                    if((x.Position - Position).Length() < 1) isThere = true;
                }
                foreach(var x in level.dynamicObjects){
                    if((x.Position - Position).Length() < 1) isThere = true;
                }
                foreach(var x in level.cinematicObjects){
                    if((x.Position - Position).Length() < 1) isThere = true;
                }
                if(!isThere) {
                    switch(currentAddables) {
                        case LevelAddables.Tile:
                            level.AddSolidTile(new TileInfo(
                                indices[(int) LevelAddables.Tile] % 16, indices[(int) LevelAddables.Tile]/16), (int) Math.Floor(Position.X/16), (int) Math.Floor(Position.Y/16));
                            break;
                        case LevelAddables.Cinematic:
                            GetCinematic(indices[(int) LevelAddables.Cinematic]).Add(level);
                            break;
                        case LevelAddables.PlayerPos:
                            level.spawnPosition = Position;
                            break;
                        case LevelAddables.CameraPos:
                            level.camPos = Position;
                            break;
                    }
                }
            }
            if(haveBecomeActive.Contains(InputEvent.Remove)) {
                for (int i = 0; i < level.dynamicObjects.Count; i++)
                {
                    if((level.dynamicObjects[i].Position - Position).Length() < 1) {
                        level.dynamicObjects.RemoveAt(i);
                        break;
                    }
                }
                for (int i = 0; i < level.cinematicObjects.Count; i++)
                {
                    if((level.cinematicObjects[i].Position - Position).Length() < 1) {
                        level.cinematicObjects.RemoveAt(i);
                        break;
                    }
                }
                for (int i = 0; i < level.tiles.Count; i++)
                {
                    if((level.tiles[i].Position - Position).Length() < 1) {
                        level.tiles.RemoveAt(i);
                        break;
                    }
                }
                for (int i = 0; i < level.staticColliders.Count; i++)
                {
                    if((level.staticColliders[i].TopLeft - Position).Length() < 1) {
                        level.staticColliders.RemoveAt(i);
                        break;
                    }
                }
                
            }
            if(haveBecomeActive.Contains(InputEvent.NextThing) && indices[indexIndex] < 256) {
                indices[indexIndex]++;
            }
            if(haveBecomeActive.Contains(InputEvent.PreviousThing) && indices[indexIndex] > 0) {
                indices[indexIndex]--;
            }
                
       }

        public void Draw(float totalSeconds)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            
            foreach(Tile tile in level.tiles) {
                tile.Draw(tileRenderer, resourceAccessor, camPos);
            }           
            background.Draw(backgroundRenderer, resourceAccessor, camPos);
            foreach(DynamicObject dynamicObject in level.dynamicObjects) {
                dynamicObject.Draw(dynamicRenderer, resourceAccessor, camPos);
            }
            foreach(CinematicObject cinematicObject in level.cinematicObjects) {
                cinematicObject.Draw(dynamicRenderer, resourceAccessor, camPos);
            }
            background.Draw(backgroundRenderer, resourceAccessor, camPos);
            switch(currentAddables) {
                case LevelAddables.Tile:
                    GetTile(indices[indexIndex]).Draw(tileRenderer, resourceAccessor, camPos);
                    break;
                case LevelAddables.Cinematic:
                    GetCinematic(indices[indexIndex]).Draw(dynamicRenderer, resourceAccessor, camPos);
                    break;
                case LevelAddables.PlayerPos:
                    GetTile(101).Draw(tileRenderer, resourceAccessor, camPos); //character sprite
                    break;
                case LevelAddables.CameraPos:
                    GetTile(12).Draw(tileRenderer, resourceAccessor, camPos); //remotely technological looking sprite
                    break;                 
            }
            //new Tile(new TileInfo(tileIndex % 16, tileIndex/16), (int) Math.Floor(Position.X/16), (int) Math.Floor(Position.Y/16)).Draw(tileRenderer, resourceAccessor, camPos);
            spriteBatch.End();
        }
        
        public Level RetrieveLevel() {
            return level;
        }
    }
}
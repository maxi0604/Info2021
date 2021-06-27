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

        private Tile GetTile(int count, Vector2 position) {
            return new Tile(new TileInfo(count % 16, count / 16),
                (int) position.X/16, (int) position.Y/16);
        }

        private CinematicObject GetCinematic(int count, Vector2 position) {
            CinematicObject[] allCinems =
            {new Goal(position),
            new Spikes(position, 0),
            new Spikes(position, 1),
            new Spikes(position, 2),
            new Spikes(position, 3),
            new Spring(position, 0),
            new Spring(position, 1),
            new Spring(position, 2),
            new Spring(position, 3),
            new MovingPlatform(position, 64 * Vector2.UnitX, 3),
            new MovingPlatform(position, 64 * Vector2.UnitY, 3),
            new MovingPlatform(position, -64 * Vector2.UnitX, 3),
            new MovingPlatform(position, -64 * Vector2.UnitY, 3)};
            return allCinems[count % allCinems.Length];
        }

        public LevelEditor(ResourceAccessor resourceAccessor, SpriteBatch spriteBatch) {
            this.resourceAccessor = resourceAccessor;
            this.spriteBatch = spriteBatch;
            tileRenderer = new TileRenderer(spriteBatch);
            backgroundRenderer = new BackgroundRenderer(spriteBatch);
            dynamicRenderer = new DynamicRenderer(spriteBatch);
            background = new Background("background1");
            indices = new int[] {0,0,0,0};
        }
        public void Initialize(Level level) {
            this.level = level;
            level.dynamicObjects.RemoveAll(x => x is Player);
            
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
                            GetCinematic(indices[(int) LevelAddables.Cinematic], Position).Add(level);
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
            GetTile(12, level.camPos).Draw(tileRenderer, resourceAccessor, camPos);
            GetTile(109, level.spawnPosition).Draw(tileRenderer, resourceAccessor, camPos);
            background.Draw(backgroundRenderer, resourceAccessor, camPos);
            switch(currentAddables) {
                case LevelAddables.Tile:
                    GetTile(indices[indexIndex], Position).Draw(tileRenderer, resourceAccessor, camPos);
                    break;
                case LevelAddables.Cinematic:
                    GetCinematic(indices[indexIndex], Position).Draw(dynamicRenderer, resourceAccessor, camPos);
                    break;
                case LevelAddables.PlayerPos:
                    GetTile(109, Position).Draw(tileRenderer, resourceAccessor, camPos); //character sprite
                    break;
                case LevelAddables.CameraPos:
                    GetTile(12, Position).Draw(tileRenderer, resourceAccessor, camPos); //remotely technological looking sprite
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
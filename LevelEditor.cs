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

        Level level;
        Background background;

        Vector2 Position = Vector2.Zero;
        private static HashSet<InputEvent> haveBecomeActive = new HashSet<InputEvent>();

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
        }
        public void Initialize() {
            if(level.dynamicObjects.Count > 0 && level.dynamicObjects[0] is Player) {
                level.dynamicObjects.RemoveAt(0);
            }
        }
        public void Update(float gameTime) {

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
                if(!isThere)
                    level.AddSolidTile(new TileInfo(2,0), (int) Math.Floor(Position.X/16), (int) Math.Floor(Position.Y/16));
                
            }
            if(haveBecomeActive.Contains(InputEvent.Remove)) {
                foreach(var x in level.dynamicObjects){
                   if((x.Position - Position).Length() < 1) level.dynamicObjects.Remove(x);
                    break;
                }
                foreach(var x in level.cinematicObjects){
                    if((x.Position - Position).Length() < 1) level.cinematicObjects.Remove(x);
                    break;
                }
                foreach(var x in level.tiles){
                    if((x.Position - Position).Length() < 1) level.tiles.Remove(x);
                    break;
                }
                foreach(var x in level.staticColliders){
                    if((x.TopLeft - Position).Length() < 1) level.staticColliders.Remove(x);
                    break;
                }
                
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
            new Tile(new TileInfo(15,0), (int) Math.Floor(Position.X/16), (int) Math.Floor(Position.Y/16)).Draw(tileRenderer, resourceAccessor, camPos);
            spriteBatch.End();
        }
        
        public Level RetrieveLevel() {
            return level;
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Info2021
{
    public class LevelRunner
    {
        ResourceAccessor resourceAccessor;
        SpriteBatch spriteBatch;
        Vector2 camPos;
        TileRenderer tileRenderer;
        BackgroundRenderer backgroundRenderer;
        DynamicRenderer dynamicRenderer;

        List<Tile> tiles;
        List<StaticCollider> staticColliders;
        List<DynamicObject> dynamicObjects;
        Background background;
        public Vector2 CamPos { get => camPos; set => camPos = value; }

        private Player player;
        private bool initialized = false;

        public LevelRunner(ResourceAccessor resourceAccessor, SpriteBatch spriteBatch) {
            this.resourceAccessor = resourceAccessor;
            this.spriteBatch = spriteBatch;
            tileRenderer = new TileRenderer(spriteBatch);
            backgroundRenderer = new BackgroundRenderer(spriteBatch);
            dynamicRenderer = new DynamicRenderer(spriteBatch);
        }
        public void Initialize(Level level) {

            initialized = true;
            camPos = level.camPos;
            dynamicObjects = level.dynamicObjects;
            staticColliders = level.staticColliders;
            background = level.background;
            tiles = level.tiles;
            player = new Player(level.spawnPosition);
            
            dynamicObjects.Insert(0, player);
        }
        public void Update(float gameTime) {
            if(!initialized) throw new System.InvalidOperationException();

            if(player.Position.X - camPos.X > 640) {
                
                camPos.X += 640;
            }
            if(player.Position.X - camPos.X < -16) {
                
                camPos.X -= 640;
            }
            if(player.Position.Y - camPos.Y > 376) {
                
                camPos.Y += 360;
            }
            if(player.Position.Y - camPos.Y < -16) {
                
                camPos.Y -= 360;
            }
            player.VelPos = player.VelPos.ApplyVelocity(gameTime);
               for (int i = 0; i < dynamicObjects.Count; i++) {
                // Do normal physics...
                dynamicObjects[i].Update(gameTime);
            }
            // Then resolve collisions as suggested in https://spicyyoghurt.com/tutorials/html5-javascript-game-development/collision-detection-physics
            // TODO: Generalize to multiple dynamic colliders and possibly even dynamic collider - dynamic collider collision.
            foreach(StaticCollider staticCollider in staticColliders) {
                player.Collider.CollideWith(staticCollider);
            }
             
        }

        public void Draw(float dt) {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            
            foreach(Tile tile in tiles) {
                tile.Draw(tileRenderer, resourceAccessor, camPos);
            }
            
            background.Draw(backgroundRenderer, resourceAccessor, camPos);
            foreach(DynamicObject dynamicObject in dynamicObjects) {
                dynamicObject.Draw(dynamicRenderer, resourceAccessor, camPos);
            }
            spriteBatch.End();
        }
    }
}
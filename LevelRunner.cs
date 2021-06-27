using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Info2021
{
    class LevelRunner
    {
        ResourceAccessor resourceAccessor;
        SpriteBatch spriteBatch;
        Vector2 camPos;
        // TODO: Make this less janky
        Vector2 targetCamPos;
        Vector2 oldCamPos;
        int camTransFrames;
        const int MAX_TRANS_FRAMES = 20;
        TileRenderer tileRenderer;
        BackgroundRenderer backgroundRenderer;
        DynamicRenderer dynamicRenderer;

        List<Tile> tiles;
        List<StaticCollider> staticColliders;
        List<DynamicObject> dynamicObjects;

        List<CinematicObject> cinematicObjects;
        Background background;

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
            oldCamPos = camPos;
            targetCamPos = camPos;
            dynamicObjects = level.dynamicObjects;
            staticColliders = level.staticColliders;
            cinematicObjects = level.cinematicObjects;
            background = level.background;
            tiles = level.tiles;
            player = new Player(level.spawnPosition);
            
            dynamicObjects.Insert(0, player);
        }
        public void Update(float gameTime) {
            if(!initialized) throw new System.InvalidOperationException();

            bool transition = false;
            // change screen
            if(player.Position.X - targetCamPos.X > 640) {
                targetCamPos.X += 640;
                transition = true;
            }
            if(player.Position.X - targetCamPos.X < -16) {
                targetCamPos.X -= 640;
                transition = true;
            }
            if(player.Position.Y - targetCamPos.Y > 376) {
                targetCamPos.Y += 360;
                transition = true;
            }
            if(player.Position.Y - targetCamPos.Y < -16) {
                targetCamPos.Y -= 360;
                transition = true;
            }

            if (transition && camTransFrames == 0) {
                camTransFrames = MAX_TRANS_FRAMES;
            }

            if (camTransFrames > 0) {
                camPos = Vector2.Lerp(oldCamPos, targetCamPos, 1 - ((float)camTransFrames)/MAX_TRANS_FRAMES);
                camTransFrames--;
            }
            else {
                oldCamPos = targetCamPos;
            }
            for (int i = 0; i < dynamicObjects.Count; i++) {
                // Do normal physics...
                dynamicObjects[i].Update(gameTime, player);
            }
            foreach(CinematicObject cinematicObject in cinematicObjects) {
                cinematicObject.Update(gameTime, player);
                cinematicObject.CCollider.CollideWith(player);
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
            foreach(CinematicObject cinematicObject in cinematicObjects) {
                cinematicObject.Draw(dynamicRenderer, resourceAccessor, camPos);
            }
            spriteBatch.End();
        }
        public bool IsAlive() {
            return player.IsAlive();
        }
        public bool HasReachedGoal()
        {
            return player.HasBeatLevel();
        }
    }

    
}
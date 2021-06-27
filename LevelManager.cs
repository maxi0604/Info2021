using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace Info2021
{
    abstract class LevelManager
    {
        
        protected ResourceAccessor resourceAccessor;
        protected SpriteBatch spriteBatch;
        protected Vector2 camPos;

        protected TileRenderer tileRenderer;
        protected BackgroundRenderer backgroundRenderer;
        protected DynamicRenderer dynamicRenderer;

        public LevelManager(ResourceAccessor resourceAccessor, SpriteBatch spriteBatch) {
            this.resourceAccessor = resourceAccessor;
            this.spriteBatch = spriteBatch;
            tileRenderer = new TileRenderer(spriteBatch);
            backgroundRenderer = new BackgroundRenderer(spriteBatch);
            dynamicRenderer = new DynamicRenderer(spriteBatch);
        }

        protected void DrawObjects(List<Tile> tiles, Background background,
            List<DynamicObject> dynamicObjects, List<CinematicObject> cinematicObjects) {
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
        }
    }
}
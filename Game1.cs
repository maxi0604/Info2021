using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Info2021
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D ballTexture;
        Player player;
        private List<Tile> tiles = new List<Tile>();
        private List<IUpdateable> updateables = new List<IUpdateable>();
        TileRenderer tileRenderer;
        BackgroundRenderer backgroundRenderer;
        ResourceAccessor resourceAccessor;
        private Vector2 camPos = Vector2.Zero;
        private Background background;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            resourceAccessor = new ResourceAccessor(this);
        }

       
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            player = new Player();
            updateables.Add(player);
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.ApplyChanges();
            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = Content.Load<Texture2D>("Character");
            tileRenderer = new TileRenderer(_spriteBatch);
            backgroundRenderer = new BackgroundRenderer(_spriteBatch);
            background = new Background(Content.Load<Texture2D>("640x360"));
            for(int i = 0; i < 30; i++) {
                tiles.Add(new Tile(new TileInfo(this, "Character"), i, 15));
            }
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputManager.IsActive(InputEvent.Escape))
                Exit();

            // TODO: Add your update logic here
            for (int i = 0; i < updateables.Count; i++) {
                updateables[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            if(player.Position.X - camPos.X > 640) {
                System.Threading.Thread.Sleep(100);
                camPos.X += 640;
            }
            if(player.Position.X - camPos.X < -16) {
                System.Threading.Thread.Sleep(100);
                camPos.X -= 640;
            }
            if(player.Position.Y - camPos.Y > 376) {
                System.Threading.Thread.Sleep(100);
                camPos.X += 360;
            }
            if(player.Position.Y - camPos.Y < -16) {
                System.Threading.Thread.Sleep(100);
                camPos.X -= 360;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            Texture2D SimpleTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);

            int[] pixel = {0xFFFFFF}; 
            SimpleTexture.SetData<int> (pixel, 0, SimpleTexture.Width * SimpleTexture.Height);
            
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            
            foreach(Tile tile in tiles) {
                tile.Draw(tileRenderer, resourceAccessor, camPos);
            }
            
            background.Draw(backgroundRenderer, resourceAccessor, camPos);
            _spriteBatch.Draw(ballTexture, 2 * player.Position, null, Color.White, 0, camPos, 2, SpriteEffects.None, 0);
            
            _spriteBatch.End();
            base.Draw(gameTime);
            
        }


    }
}

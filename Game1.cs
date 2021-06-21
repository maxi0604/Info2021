using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Info2021
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public ResourceAccessor resourceAccessor;
        
        private GameState gameState = GameState.Init;
        private LevelRunner levelRunner;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            resourceAccessor = new ResourceAccessor(this);
        }

       
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.ApplyChanges();
            // TODO: Remove test.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            levelRunner = new LevelRunner(resourceAccessor, spriteBatch);
            base.Initialize();

        }

        protected override void LoadContent()
        {
            
            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputManager.IsActive(InputEvent.Escape))
                Exit();

            switch (gameState)
            {
                case GameState.Init:
                    Level firstLevel = FirstLevel.GetLevel(this);
                    levelRunner.Initialize(firstLevel);
                    gameState = GameState.InLevel;
                    break;
                case GameState.InLevel:
                    levelRunner.Update((float) gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                default:
                    break;
            }
            
            
             
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            switch (gameState)
            {
                case GameState.Init:
                    break;
                case GameState.InLevel:
                    levelRunner.Draw((float) gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                default:
                    break;
            }
            
            base.Draw(gameTime);
            
        }


    }
}

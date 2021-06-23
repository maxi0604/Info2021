using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Info2021
{
    class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public ResourceAccessor resourceAccessor;
        
        private Menu menu;
        private PauseMenu pauseMenu;
        private GameState gameState = GameState.Menu;
        private LevelRunner levelRunner;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

       
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.ApplyChanges();
            // TODO: Remove test.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D spriteSheet = Content.Load<Texture2D>("spritesheet");
            int xc, yc;
            Texture2D[] textures = TextureHelper.Split(spriteSheet, 16, 16, out xc, out yc);
            
            resourceAccessor = new ResourceAccessor(this, textures, xc, yc);
            levelRunner = new LevelRunner(resourceAccessor, spriteBatch);
            menu = new Menu(spriteBatch, resourceAccessor);
            pauseMenu = new PauseMenu(spriteBatch, resourceAccessor);
            base.Initialize();

        }

        protected override void LoadContent()
        {
            
            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {


            switch (gameState)
            {
                case GameState.Menu:
                    //TODO: menu
                    menu.Update();
                    MenuItem item;
                    if(menu.HasBeenSelected(out item)) {
                        switch(item) {
                            case MenuItem.LevelSelect:
                                gameState = GameState.Init;
                                break;
                            case MenuItem.LevelEdit:
                                //TODO
                                break;
                            case MenuItem.Settings:
                                //TODO
                                break;
                            case MenuItem.Exit:
                                Exit();
                                break;
                            }
                    }
                    break;
                case GameState.Pause:
                    //TODO: menu
                    pauseMenu.Update();
                    PauseMenuItem pauseItem;
                    if(pauseMenu.HasBeenSelected(out pauseItem)) {
                        switch(pauseItem) {
                            case PauseMenuItem.Unpause:
                                gameState = GameState.InLevel;
                                break;
                            case PauseMenuItem.Retry:
                                gameState = GameState.Init;
                                break;
                            case PauseMenuItem.Settings:
                                //TODO
                                break;
                            case PauseMenuItem.MainMenu:
                                gameState = GameState.Menu;
                                break;
                            }
                    }
                    break;
                case GameState.Init:
                    Level firstLevel = FirstLevel.GetLevel(this);
                    levelRunner.Initialize(firstLevel);
                    gameState = GameState.InLevel;
                    break;
                case GameState.InLevel:
                    levelRunner.Update((float) gameTime.ElapsedGameTime.TotalSeconds);
                    if(InputManager.IsActive(InputEvent.Escape))
                        gameState = GameState.Pause;
                    if(!levelRunner.IsAlive())
                        gameState = GameState.Dead;
                    if(levelRunner.HasReachedGoal())
                        gameState = GameState.BeatLevel;
                    break;
                case GameState.Dead:
                    gameState = GameState.Init;
                    break;
                case GameState.BeatLevel:
                    gameState = GameState.Menu;
                    break;
                default:
                    break;
            }        

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.White);
            switch (gameState)
            {
                case GameState.Menu:
                    menu.Draw((float) gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                case GameState.Init:
                    break;
                case GameState.InLevel:
                    levelRunner.Draw((float) gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                case GameState.BeatLevel:
                    break;
                case GameState.Pause:
                    pauseMenu.Draw((float) gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                default:
                    break;
            }
            
            base.Draw(gameTime);
            
        }


    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Info2021 {
    class Game1 : Game {
        public ResourceAccessor ResourceAccessor { get; private set; }
        public SpriteFont Font { get; private set; }
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Menu menu;
        private PauseMenu pauseMenu;
        private EditSelectionMenu editSelectionMenu;
        private EditorMenu editorMenu;

        private LevelSelectMenu levelSelectMenu;
        private GameState gameState = GameState.Menu;
        private LevelRunner levelRunner;
        private Level level;
        private LevelEditor levelEditor;
        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize() {
            // Fixed resolution to avoid scaling
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.ApplyChanges();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Preloading sprite sheet for performance reasons
            Texture2D spriteSheet = Content.Load<Texture2D>("spritesheet");
            int xc, yc;
            Texture2D[] textures = TextureHelper.Split(spriteSheet, 16, 16, out xc, out yc);

            ResourceAccessor = new ResourceAccessor(this, textures, xc, yc);
            levelRunner = new LevelRunner(ResourceAccessor, spriteBatch);
            menu = new Menu(spriteBatch, ResourceAccessor);
            pauseMenu = new PauseMenu(spriteBatch, ResourceAccessor);
            levelEditor = new LevelEditor(ResourceAccessor, spriteBatch);
            editSelectionMenu = new EditSelectionMenu(spriteBatch, ResourceAccessor);
            editorMenu = new EditorMenu(spriteBatch, ResourceAccessor);
            levelSelectMenu = new LevelSelectMenu(spriteBatch, ResourceAccessor);
            try {
                level = Level.Load("Levels/edit.lvl");
            }
            catch (System.Exception) {

                level = new Level(Vector2.Zero, Vector2.Zero, new List<Tile>(),
                new List<StaticCollider>(), new List<DynamicObject>(), new List<CinematicObject>(),
                new Background("background1"));
            }

            Font = Content.Load<SpriteFont>("MontserratBold");
            base.Initialize();

        }

        protected override void Update(GameTime gameTime) {


            switch (gameState) {
                case GameState.Menu:
                    //TODO: menu
                    menu.Update();
                    MenuItem item;
                    if (menu.HasBeenSelected(out item)) {
                        switch (item) {
                            case MenuItem.LevelSelect:
                                // prevents some double tapping bug
                                System.Threading.Thread.Sleep(50);
                                gameState = GameState.LevelSelect;
                                break;
                            case MenuItem.LevelEdit:
                                gameState = GameState.Edit;
                                level = Level.Load("Levels/edit.lvl");
                                levelEditor.Initialize(level);
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
                    if (pauseMenu.HasBeenSelected(out pauseItem)) {
                        switch (pauseItem) {
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

                    //Level firstLevel = FirstLevel.GetLevel(this);
                    levelRunner.Initialize(level);
                    gameState = GameState.InLevel;
                    break;
                case GameState.InLevel:
                    levelRunner.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                    if (InputManager.IsActive(InputEvent.Escape))
                        gameState = GameState.Pause;
                    if (!levelRunner.IsAlive())
                        gameState = GameState.Dead;
                    if (levelRunner.HasReachedGoal())
                        gameState = GameState.BeatLevel;
                    break;
                case GameState.Dead:
                    gameState = GameState.Init;
                    break;
                case GameState.BeatLevel:
                    gameState = GameState.Menu;
                    break;
                case GameState.Edit:
                    if (InputManager.IsActive(InputEvent.Escape)) {
                        gameState = GameState.EditMenu;
                    }
                    if (InputManager.IsActive(InputEvent.Menu)) {
                        gameState = GameState.EditSelectionMenu;
                    }
                    levelEditor.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                case GameState.EditSelectionMenu:
                    editSelectionMenu.Update();
                    LevelAddables addItem;
                    if (editSelectionMenu.HasBeenSelected(out addItem)) {
                        levelEditor.currentAddables = addItem;
                        gameState = GameState.Edit;
                    }
                    break;
                case GameState.EditMenu:
                    editorMenu.Update();
                    EditorMenuItem editorMenuItem;
                    if (editorMenu.HasBeenSelected(out editorMenuItem)) {
                        switch (editorMenuItem) {
                            case EditorMenuItem.Continue:
                                gameState = GameState.Edit;
                                break;
                            case EditorMenuItem.PlaySave:
                                level = levelEditor.RetrieveLevel();
                                level.Save("Levels/edit.lvl");
                                gameState = GameState.Init;
                                break;
                            case EditorMenuItem.Save:
                                level.Save("Levels/edit.lvl");
                                break;
                            case EditorMenuItem.Backup:
                                level.Save("Levels/backup" + System.DateTime.Now.ToString("s") + ".lvl");
                                break;
                            case EditorMenuItem.Reset:
                                level = new Level(Vector2.Zero, Vector2.Zero,
                                new List<Tile>(), new List<StaticCollider>(), new List<DynamicObject>(),
                                new List<CinematicObject>(), new Background("background1"));
                                levelEditor.Initialize(level);
                                gameState = GameState.Edit;
                                break;
                            case EditorMenuItem.MainMenu:
                                gameState = GameState.Menu;
                                break;
                        }
                    }
                    break;
                case GameState.LevelSelect:
                    levelSelectMenu.Update();
                    int selectedLevel;
                    if (levelSelectMenu.HasBeenSelected(out selectedLevel)) {
                        if (selectedLevel == 0) {
                            gameState = GameState.Menu;
                        }
                        else {
                            level = Level.Load("Levels/level" + selectedLevel.ToString() + ".lvl");
                            gameState = GameState.Init;

                        }
                    }
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            graphics.GraphicsDevice.Clear(Color.White);
            switch (gameState) {
                case GameState.Menu:
                    menu.Draw((float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                case GameState.Init:
                    break;
                case GameState.InLevel:
                    levelRunner.Draw((float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                case GameState.BeatLevel:
                    break;
                case GameState.Pause:
                    pauseMenu.Draw((float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                case GameState.Edit:
                    levelEditor.Draw((float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                case GameState.EditSelectionMenu:
                    editSelectionMenu.Draw((float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                case GameState.EditMenu:
                    editorMenu.Draw((float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                case GameState.LevelSelect:
                    levelSelectMenu.Draw((float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                default:
                    break;
            }

            base.Draw(gameTime);

        }


    }
}

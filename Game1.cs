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

        private List<IUpdateable> updateables = new List<IUpdateable>();
        private List<StaticCollider> staticColliders = new List<StaticCollider>();
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player();
            updateables.Add(player);
            // TODO: Remove test.
            staticColliders.Add(new StaticCollider(new Vector2(0f, 200f), new Vector2(200f, 300f)));
            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = Content.Load<Texture2D>("Character");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputManager.IsActive(InputEvent.Escape))
                Exit();

            for (int i = 0; i < updateables.Count; i++) {
                // Do normal physics...
                updateables[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            // Then resolve collisions as suggested in https://spicyyoghurt.com/tutorials/html5-javascript-game-development/collision-detection-physics
            // TODO: Generalize to multiple dynamic colliders and possibly even dynamic collider - dynamic collider collision.
            for (int i = 0; i < staticColliders.Count; i++) {
                player.Collider.CollideWith(staticColliders[i]);
            }

            player.VelPos = player.VelPos.ApplyVelocity(1/60f);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spriteBatch.Begin();
            _spriteBatch.Draw(ballTexture, player.Position, Color.White);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

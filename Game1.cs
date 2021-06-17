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
        private List<ICollidable> attachedColliders = new List<ICollidable>();
        private List<ICollidable> staticColliders = new List<ICollidable>();
        
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
            attachedColliders.Add(player);
            var statictest = new StaticCollider(new Vector2(0f, 200f), new Vector2(200f, 400f));
            staticColliders.Add(statictest);
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

            // TODO: Add your update logic here
            for (int i = 0; i < updateables.Count; i++) {
                updateables[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            for (int i = 0; i < attachedColliders.Count; i++) {
                var attached = attachedColliders[i];
                for (int j = 0; j < staticColliders.Count; j++) {
                    var result = attached.Collider.CollideWith(staticColliders[j].Collider);
                    if (result is Vector2 actualResult)
                        attached.VelPos = new VelPos(attached.VelPos.P, Vector2.Multiply(attached.VelPos.V, actualResult));

                    attached.VelPos = attached.VelPos.ApplyVelocity((float)gameTime.ElapsedGameTime.TotalSeconds);
                }
            }
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

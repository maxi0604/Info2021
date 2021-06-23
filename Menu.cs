using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Info2021
{
    using Interfaces;
    class Menu : IDrawable
    {
        private SpriteBatch spriteBatch;
        private ResourceAccessor resourceAccessor;
        private MenuRenderer menuRenderer;
        private int activeIndex = 0;
        public MenuItem ActiveItem => AllItems[activeIndex];
        private bool pressedJump = false;
        private bool oldJump = false;

        private bool pressedUp = false;
        private bool oldUp = false;

        private bool pressedDown = false;
        private bool oldDown = false;
        public static MenuItem[] AllItems = {MenuItem.LevelSelect, MenuItem.LevelEdit, MenuItem.Settings, MenuItem.Exit};

        public Menu(SpriteBatch spriteBatch, ResourceAccessor resourceAccessor)
        {
            this.spriteBatch = spriteBatch;
            this.resourceAccessor = resourceAccessor;
            menuRenderer = new MenuRenderer(spriteBatch);
        }

        public MenuItem? HasBeenSelected() {
            if(!oldJump && pressedJump)
                return ActiveItem;
            else
                return null;
        }

        public void Update() {
            oldJump = pressedJump;
            pressedJump = InputManager.IsActive(InputEvent.Jump);
            oldUp = pressedUp;
            pressedUp = InputManager.IsActive(InputEvent.Up);
            oldDown = pressedDown;
            pressedDown = InputManager.IsActive(InputEvent.Down);
            if(pressedDown && !oldDown && activeIndex < AllItems.Length - 1) activeIndex++;
            if(pressedUp && !oldUp && activeIndex > 0) activeIndex--;
        }

        public void Draw(IRenderer renderer, ResourceAccessor accessor, Vector2 camPos)
        {
            renderer.Draw(camPos, accessor.GetSprite(14, 0), new Vector2(240, activeIndex * 50 + 80), 0, Vector2.Zero, 1, 0.0001f);
        }

        public void Draw(float dt) {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            Draw(menuRenderer, resourceAccessor, Vector2.Zero);
            spriteBatch.End();
        }
    }

}
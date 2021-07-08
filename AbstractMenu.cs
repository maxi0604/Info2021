using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
namespace Info2021 {
    abstract class AbstractMenu<A> {

        // A represents the possible menu items without any further restrictions
        protected SpriteBatch spriteBatch;
        protected ResourceAccessor resourceAccessor;
        protected MenuRenderer menuRenderer;
        // selected menu option number
        protected int activeIndex = 0;
        // selected menu option
        public A ActiveItem { get { return AllItems[activeIndex]; } }
        // list of displayed Texts for each menu
        public abstract string[] Texts { get; }
        protected bool pressedJump = false;
        // was it pressed in the last Frame
        protected bool oldJump = false;

        protected bool pressedUp = false;
        // was it pressed in the last Frame
        protected bool oldUp = false;

        protected bool pressedDown = false;
        // was it pressed in the last Frame
        protected bool oldDown = false;
        // all options of one specific menu-state?
        public abstract A[] AllItems { get; }

        public AbstractMenu(SpriteBatch spriteBatch, ResourceAccessor resourceAccessor) {
            this.spriteBatch = spriteBatch;
            this.resourceAccessor = resourceAccessor;
            menuRenderer = new MenuRenderer(spriteBatch);
        }
        // gets called in Game1.cs
        // item becomes ActiveItem, which is determined by acticeIndex(public A ActiveItem { get { return AllItems[activeIndex]; } }), which is updated by Update()
        public bool HasBeenSelected(out A item) {
            item = ActiveItem;
            return !oldJump && pressedJump;
        }


        public void Update() {
            // check whether button is now pressed and was not pressed on last frame
            // oldJump takes value of previos frame, oldUp takes ...
            oldJump = pressedJump;
            // current value gets checked ...
            pressedJump = InputManager.IsActive(InputEvent.Jump);
            oldUp = pressedUp;
            pressedUp = InputManager.IsActive(InputEvent.Up);
            oldDown = pressedDown;
            pressedDown = InputManager.IsActive(InputEvent.Down);
            // when down button is pressed and was not pressed in previous frame and the next index would not exeed the number possible indexes
            if (pressedDown && !oldDown && activeIndex < AllItems.Length - 1) activeIndex++;
            if (pressedUp && !oldUp && activeIndex > 0) activeIndex--;
        }

        public void Draw(MenuRenderer renderer, ResourceAccessor accessor, Vector2 camPos) {
            for (int i = 0; i < AllItems.Length; i++) {
                // Draw selected text in black, unselected in blue
                renderer.DrawText(new Vector2(100, i * 50 + 20), accessor.Font, Texts[i], i == activeIndex ? Color.Black : Color.Blue);
            }

        }

        public void Draw(float dt) {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            Draw(menuRenderer, resourceAccessor, Vector2.Zero);
            spriteBatch.End();
        }
    }

}

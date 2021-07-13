// Gehört zu Johannes

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Info2021 {

    class PauseMenu : AbstractMenu<PauseMenuItem> {
        public override PauseMenuItem[] AllItems => new PauseMenuItem[] { PauseMenuItem.Unpause, PauseMenuItem.Retry, PauseMenuItem.MainMenu };
        public override string[] Texts => new string[] { "Resume", "Retry", "Main Menu" };

        public PauseMenu(SpriteBatch spriteBatch, ResourceAccessor resourceAccessor) : base(spriteBatch, resourceAccessor) {

        }

    }

}
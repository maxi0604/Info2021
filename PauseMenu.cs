using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Info2021
{
    
    class PauseMenu : AbstractMenu<PauseMenuItem>
    {
        public override PauseMenuItem[] AllItems {get => new PauseMenuItem[] {PauseMenuItem.Unpause, PauseMenuItem.Retry, PauseMenuItem.Settings, PauseMenuItem.MainMenu}; }
        

        public PauseMenu(SpriteBatch spriteBatch, ResourceAccessor resourceAccessor) : base(spriteBatch, resourceAccessor)
        {
         
        }

    }

}
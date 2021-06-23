using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Info2021.Interfaces;
namespace Info2021
{
    
    class PauseMenu : AbstractMenu<PauseMenuItem>
    {
        public override PauseMenuItem[] AllItems {get {return new PauseMenuItem[] {PauseMenuItem.Unpause, PauseMenuItem.Retry, PauseMenuItem.Settings, PauseMenuItem.MainMenu}; }}
        

        public PauseMenu(SpriteBatch spriteBatch, ResourceAccessor resourceAccessor) : base(spriteBatch, resourceAccessor)
        {
         
        }

    }

}
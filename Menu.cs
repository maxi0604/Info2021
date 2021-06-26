using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Info2021
{
    
    class Menu : AbstractMenu<MenuItem>
    {
        public override MenuItem[] AllItems => new MenuItem[] {MenuItem.LevelSelect, MenuItem.LevelEdit, MenuItem.Settings, MenuItem.Exit};
        public override string[] Texts => new string[] {"Play", "Edit", "Settings", "Exit"};

        public Menu(SpriteBatch spriteBatch, ResourceAccessor resourceAccessor) : base(spriteBatch, resourceAccessor)
        {
         
        }

    }

}
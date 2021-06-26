using Microsoft.Xna.Framework.Graphics;
namespace Info2021
{
    class EditSelectionMenu : AbstractMenu<LevelAddables>
    {
        public override LevelAddables[] AllItems => (LevelAddables[])System.Enum.GetValues(typeof(LevelAddables));

        public EditSelectionMenu(SpriteBatch spriteBatch, ResourceAccessor resourceAccessor) : base(spriteBatch, resourceAccessor)
        {
         
        }
    }
}
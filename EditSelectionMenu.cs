using Microsoft.Xna.Framework.Graphics;
namespace Info2021 {
    class EditSelectionMenu : AbstractMenu<LevelAddables> {
        public override LevelAddables[] AllItems => (LevelAddables[])System.Enum.GetValues(typeof(LevelAddables));
        public override string[] Texts => new string[] { "Tiles", "Cinematics", "Spawn", "Cam" };
        public EditSelectionMenu(SpriteBatch spriteBatch, ResourceAccessor resourceAccessor) : base(spriteBatch, resourceAccessor) {

        }
    }
}
using Microsoft.Xna.Framework.Graphics;
namespace Info2021 {
    class LevelSelectMenu : AbstractMenu<int> {
        public override int[] AllItems => new int[] { 1, 2, 3, 4, 5, 0 };

        public override string[] Texts => new string[]
            {"Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Main Menu"};

        public LevelSelectMenu(SpriteBatch spriteBatch, ResourceAccessor resourceAccessor) : base(spriteBatch, resourceAccessor) {

        }
    }
}
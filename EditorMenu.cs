// Gehört zu Johannes
using Microsoft.Xna.Framework.Graphics;
namespace Info2021 {
    class EditorMenu : AbstractMenu<EditorMenuItem> {
        public override EditorMenuItem[] AllItems => (EditorMenuItem[])System.Enum.GetValues(typeof(EditorMenuItem));
        public override string[] Texts => new string[] { "Resume", "Save & Play", "Save", "Backup", "Reset Level", "Exit" };
        public EditorMenu(SpriteBatch spriteBatch, ResourceAccessor resourceAccessor) : base(spriteBatch, resourceAccessor) {

        }
    }
}
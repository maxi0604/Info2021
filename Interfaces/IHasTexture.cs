using Microsoft.Xna.Framework.Graphics;
namespace Info2021 {
    interface IHasTexture {
        Texture2D GetTexture(ResourceAccessor resourceAccessor);
    }
}
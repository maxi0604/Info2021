using Microsoft.Xna.Framework.Graphics;
namespace Info2021.Interfaces
{
    interface IHasTexture
    {
        Texture2D GetTexture(ResourceAccessor resourceAccessor);
    }
}
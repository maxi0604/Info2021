using Microsoft.Xna.Framework;
namespace Info2021
{
    interface IDrawable
    {
        void Draw(IRenderer renderer, ResourceAccessor accessor, Vector2 camPos);
    }
}
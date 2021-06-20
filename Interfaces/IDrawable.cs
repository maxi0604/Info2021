using Microsoft.Xna.Framework;
namespace Info2021.Interfaces
{
    public interface IDrawable
    {
        void Draw(IRenderer renderer, ResourceAccessor accessor, Vector2 camPos);
    }
}
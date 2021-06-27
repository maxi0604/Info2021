using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization;

namespace Info2021 {
    [DataContract]
    class Background : IDrawable {
        [DataMember]
        public string texturePath;

        public Background(string texturePath) {
            this.texturePath = texturePath;
        }

        public void Draw(IRenderer renderer, ResourceAccessor accessor, Vector2 camPos) {
            renderer.Draw(camPos, accessor.LoadContent<Texture2D>(texturePath), camPos, 0, Vector2.Zero, 1, 0.99f);
        }


    }
}
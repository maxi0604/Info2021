using Microsoft.Xna.Framework;
using System;
namespace Info2021
{
    public static class CamTranslator
    {
        public static (int, int) Translator(Vector2 camPos, Vector2 position)
        {
            // game -> screen scale factor is 2
            Vector2 relPos = (position - camPos) * 2;

            return ((int)Math.Floor(relPos.X), (int)Math.Floor(relPos.Y));
        }
    }
}
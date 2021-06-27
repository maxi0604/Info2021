using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace Info2021 {
    public static class InputManager {
        public static Dictionary<Keys, InputEvent> Translator { get; } = new Dictionary<Keys, InputEvent>()
            {{Keys.Left, InputEvent.Left},
             {Keys.Right, InputEvent.Right},
             {Keys.Up, InputEvent.Up},
             {Keys.Down, InputEvent.Down},
             {Keys.X, InputEvent.Jump},
             {Keys.Escape, InputEvent.Escape},
             {Keys.C, InputEvent.Remove},
             {Keys.D, InputEvent.NextThing},
             {Keys.A, InputEvent.PreviousThing},
             {Keys.Space, InputEvent.Menu},};

        public static bool IsActive(InputEvent input) {
            if (input == InputEvent.Jump && Mouse.GetState().LeftButton == ButtonState.Pressed) {
                return true;
            }
            if (input == InputEvent.Remove && Mouse.GetState().RightButton == ButtonState.Pressed) {
                return true;
            }
            var keys = Translator.Where(x => x.Value == input).Select(x => x.Key);
            return keys.Any(Keyboard.GetState().IsKeyDown);
        }

        public static Vector2 MousePos =>
            new Vector2(Mouse.GetState().X / 2, Mouse.GetState().Y / 2);

    }
}
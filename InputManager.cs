using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace Info2021
{
    public static class InputManager
    {
        public static Dictionary<Keys, InputEvent> Translator {get;} = new Dictionary<Keys, InputEvent>()
            {{Keys.Left, InputEvent.Left},
             {Keys.Right, InputEvent.Right},
             {Keys.Up, InputEvent.Up},
             {Keys.Down, InputEvent.Down},
             {Keys.X, InputEvent.Jump},
             {Keys.Escape, InputEvent.Escape},
             {Keys.Delete, InputEvent.Remove}};

        public static bool IsActive(InputEvent input) {
            var keys = Translator.Where(x => x.Value == input).Select(x => x.Key);
            return keys.Any(Keyboard.GetState().IsKeyDown);
        }       
        
    }
}
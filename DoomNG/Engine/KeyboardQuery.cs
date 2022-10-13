using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DoomNG.Engine
{
    static internal class KeyboardQuery
    {
        public enum KeyPressState { Started, Pressed, Released }
        static KeyboardState _lastState = default;
        static KeyboardState _currentState = default;

        public static bool Query(Keys key, KeyPressState state)
        {
            switch (state)
            {
                case KeyPressState.Started:
                    if (_lastState.IsKeyUp(key) && _currentState.IsKeyDown(key))
                        return true;
                    break;
                case KeyPressState.Pressed:
                    if (_currentState.IsKeyDown(key))
                        return true;
                    break;
                case KeyPressState.Released:
                    if (_lastState.IsKeyDown(key) && _currentState.IsKeyUp(key))
                        return true;
                    break;
                default: return false;
            }
            return false;
        }

        public static bool WasPressedThisFrame(Keys key) => _lastState.IsKeyUp(key) && _currentState.IsKeyDown(key);
        public static bool IsPressed(Keys key) => _currentState.IsKeyDown(key);
        public static bool WasReleasedThisFrame(Keys key) => _lastState.IsKeyDown(key) && _currentState.IsKeyUp(key);
        public static int CheckAxis(Keys negative, Keys positive) => IsPressed(positive) ? (IsPressed(negative) ? 0 : 1) : (IsPressed(negative) ? -1 : 0);

        public static void UpdateKeyboard()
        {
            _lastState = _currentState;
            _currentState = Keyboard.GetState();
        }
    }
}

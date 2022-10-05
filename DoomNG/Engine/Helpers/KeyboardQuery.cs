using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DoomNG.Engine.Helpers
{
    static internal class KeyboardQuery
    {
        public enum KeyPressState { Started, Pressed, Released }
        static KeyboardState _lastState = default;
        static KeyboardState _currentState = default;

        public static Dictionary<Keys, List<Command>> _keyStartActions = new Dictionary<Keys, List<Command>>();
        public static Dictionary<Keys, List<Command>> _keyPressActions = new Dictionary<Keys, List<Command>>();
        public static Dictionary<Keys, List<Command>> _keyReleaseActions = new Dictionary<Keys, List<Command>>();

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

        public static void AddCommand(Keys key, KeyPressState state, Command command)
        {
            switch (state)
            {
                case KeyPressState.Started:
                    SafeDictAdd(_keyStartActions, key, command);
                    break;
                case KeyPressState.Pressed:
                    SafeDictAdd(_keyPressActions, key, command);
                    break;
                case KeyPressState.Released:
                    SafeDictAdd(_keyReleaseActions, key, command);
                    break;
            }
        }

        public static void RemoveCommand(Keys key, KeyPressState state, Command command)
        {
            switch (state)
            {
                case KeyPressState.Started:
                    _keyStartActions[key].Remove(command);
                    break;
                case KeyPressState.Pressed:
                    _keyPressActions[key].Remove(command);
                    break;
                case KeyPressState.Released:
                    _keyReleaseActions[key].Remove(command);
                    break;
            }
        }

        public static void UpdateKeyboard()
        {
            _lastState = _currentState;
            _currentState = Keyboard.GetState();

            foreach(Keys key in _currentState.GetPressedKeys())
            {
                if (_keyStartActions.ContainsKey(key))
                {
                    if (WasPressedThisFrame(key))
                    {
                        foreach(Command command in _keyStartActions[key].ToList())
                        {
                            command.Execute?.Invoke();
                        }
                    }
                }

                if (_keyPressActions.ContainsKey(key))
                {
                    foreach (Command command in _keyPressActions[key].ToList())
                    {
                        command.Execute?.Invoke();
                    }
                }
                if (_keyReleaseActions.ContainsKey(key))
                {
                    if (WasReleasedThisFrame(key))
                    {
                        foreach (Command command in _keyReleaseActions[key].ToList())
                        {
                            command.Execute?.Invoke();
                        }
                    }
                }
            }
        }

        static void SafeDictAdd(Dictionary<Keys,List<Command>> dict, Keys key, Command command)
        {
            if (dict.ContainsKey(key))
            {
                dict[key].Add(command);
            }
            else
            {
               dict.Add(key, new List<Command>() { command });
            }
        }
    }
}

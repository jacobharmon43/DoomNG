using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace DoomNG.Engine
{
    internal static class TextureDistributor
    {
        static Dictionary<string, Texture2D> _setTextures = new Dictionary<string, Texture2D>();

        public static void AddTexture(string Name, Texture2D texture)
        {
            _setTextures.TryAdd(Name, texture);
        }

        public static void RemoveTexture(string Name)
        {
            _setTextures.Remove(Name);
        }

        public static Texture2D GetTexture(string Name)
        {
            if (!_setTextures.ContainsKey(Name))
                return null;
            return _setTextures[Name];
        }
    }
}
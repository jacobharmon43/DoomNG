using Microsoft.Xna.Framework;

namespace DoomNG.Engine
{
    /// <summary>
    /// Marker interface for all systems, contains single execute method
    /// </summary>
    internal interface ISystem{
        public void Execute(GameTime gameTime);
    }
}

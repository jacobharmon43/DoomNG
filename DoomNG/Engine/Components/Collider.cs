using Microsoft.Xna.Framework;

namespace DoomNG.Engine.Components
{
    /// <summary>
    /// Marker interface for all Collider types
    /// </summary>
    /// <seealso cref="DoomNG.Engine.IComponent" />
    public interface Collider : IComponent{
        public abstract Vector2[] GetVertices();
    }
}

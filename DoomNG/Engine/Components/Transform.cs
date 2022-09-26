using Microsoft.Xna.Framework;

namespace DoomNG.Engine.Components
{
    /// <summary>
    /// Stores placement and size info for entities
    /// </summary>
    /// <seealso cref="DoomNG.Engine.IComponent" />
    public class Transform2D : IComponent
    {
        public Point position;
        public Point scale;
        public float rotation;

        /// <summary>
        /// Initializes a new instance of the <see cref="Transform2D"/> class.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="rot">The rot.</param>
        public Transform2D(Point pos, Point scale, float rot)
        {
            this.position = pos;
            this.scale = scale;
            this.rotation = rot;
        }

        /// <summary>
        /// Translates the position by the  specified movement.
        /// </summary>
        /// <param name="movement">The movement.</param>
        public void Translate(Point movement)
        {
            this.position += movement;
        }
    }
}

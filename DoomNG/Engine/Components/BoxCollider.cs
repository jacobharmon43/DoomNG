using Microsoft.Xna.Framework;

namespace DoomNG.Engine.Components
{
    /// <summary>
    /// Four Vertice, Rectangle shaped collider
    /// </summary>
    /// <seealso cref="DoomNG.Engine.Components.Collider" />
    public class BoxCollider : Collider
    {
        Point[] vertices = new Point[4];
        Rectangle bounds;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoxCollider"/> class.
        /// </summary>
        /// <param name="bounds">The bounds.</param>
        public BoxCollider(Rectangle bounds)
        {
            this.bounds = bounds;
            vertices[0] = new Point(bounds.X, bounds.Y);
            vertices[1] = new Point(bounds.X + bounds.Width, bounds.Y);
            vertices[2] = new Point(bounds.X, bounds.Y + bounds.Height);
            vertices[3] = new Point(bounds.X + bounds.Width, bounds.Y + bounds.Height);
        }
    }
}

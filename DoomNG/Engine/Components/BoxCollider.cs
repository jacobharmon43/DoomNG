using Microsoft.Xna.Framework;

namespace DoomNG.Engine.Components
{
    /// <summary>
    /// Four Vertice, Rectangle shaped collider
    /// </summary>
    /// <seealso cref="DoomNG.Engine.Components.Collider" />
    public class BoxCollider : Collider, IComponent
    {
        Vector2[] vertices = new Vector2[4];
        Rectangle bounds;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoxCollider"/> class.
        /// </summary>
        /// <param name="bounds">The bounds.</param>
        public BoxCollider(Transform2D transform)
        {
            Point pos = new Point((int)transform.position.X, (int)transform.position.Y);
            Point scale = new Point((int)transform.scale.X, (int)transform.scale.Y);
            this.bounds = new Rectangle(pos, scale);
            vertices[0] = new Vector2(bounds.X, bounds.Y);
            vertices[1] = new Vector2(bounds.X + bounds.Width, bounds.Y);
            vertices[2] = new Vector2(bounds.X + bounds.Width, bounds.Y + bounds.Height);
            vertices[3] = new Vector2(bounds.X, bounds.Y + bounds.Height);
        }

        public BoxCollider(Vector2 pos, Vector2 scale)
        {
            Point pos1 = new Point((int)pos.X, (int)pos.Y);
            Point scale1 = new Point((int)scale.X, (int)scale.Y);
            this.bounds = new Rectangle(pos1, scale1);
            vertices[0] = new Vector2(bounds.X, bounds.Y);
            vertices[1] = new Vector2(bounds.X + bounds.Width, bounds.Y);
            vertices[2] = new Vector2(bounds.X + bounds.Width, bounds.Y + bounds.Height);
            vertices[3] = new Vector2(bounds.X, bounds.Y + bounds.Height);
        }

        public void UpdatePosition(Transform2D transform)
        {
            Point pos = new Point((int)transform.position.X, (int)transform.position.Y);
            Point scale = new Point((int)transform.scale.X, (int)transform.scale.Y);
            this.bounds = new Rectangle(pos, scale);
            vertices[0] = new Vector2(bounds.X, bounds.Y);
            vertices[1] = new Vector2(bounds.X + bounds.Width, bounds.Y);
            vertices[2] = new Vector2(bounds.X, bounds.Y + bounds.Height);
            vertices[3] = new Vector2(bounds.X + bounds.Width, bounds.Y + bounds.Height);
        }

        public Vector2[] GetVertices()
        {
            return vertices;
        }
    }
}

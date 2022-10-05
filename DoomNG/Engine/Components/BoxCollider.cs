using Microsoft.Xna.Framework;

namespace DoomNG.Engine.Components
{
    internal class BoxCollider : Collider, IComponent
    {
        Vector2[] vertices = new Vector2[4];
        Rectangle bounds;

        public BoxCollider()
        {

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

        public void UpdatePosition(Transform2D transform, Pivot pivot)
        {
            Point scale = new Point((int)transform.scale.X, (int)transform.scale.Y);
            Point pos = new Point((int)(transform.position.X - (pivot.X * scale.X)), (int)(transform.position.Y - (pivot.Y * scale.Y)));
            this.bounds = new Rectangle(pos, scale);
            vertices[0] = new Vector2(bounds.X, bounds.Y);
            vertices[1] = new Vector2(bounds.X + bounds.Width, bounds.Y);
            vertices[2] = new Vector2(bounds.X + bounds.Width, bounds.Y + bounds.Height);
            vertices[3] = new Vector2(bounds.X, bounds.Y + bounds.Height);
        }

        public Vector2[] GetVertices()
        {
            return vertices;
        }
    }
}

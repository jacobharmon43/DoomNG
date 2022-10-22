using Microsoft.Xna.Framework;

namespace DoomNG.Engine.Components
{
    internal class Transform2D : Component
    {
        public Vector2 position;
        public Vector2 scale;
        public float rotation;
        public Vector2 pivot;

        public Transform2D()
        {
            position = Vector2.Zero;
            scale = Vector2.Zero;
            rotation = 0;
            pivot = Vector2.Zero;
        }

        public Transform2D(Vector2 position, Vector2 scale, float rotation, Vector2 pivot)
        {
            this.position = position;
            this.scale = scale;
            this.rotation = rotation;
            this.pivot = pivot;
        }

        public Transform2D(Transform2D other)
        {
            position = other.position;
            scale = other.scale;
            rotation = other.rotation;
            pivot = other.pivot;
        }

        public void Translate(Vector2 by)
        {
            this.position += by;
        }

        public override object Clone()
        {
            return new Transform2D(this);
        }
    }
}

using Microsoft.Xna.Framework;
using System;

namespace DoomNG.Engine.Components
{
    /// <summary>
    /// Stores offsets from transform center
    /// </summary>
    internal class BoxCollider : IComponent
    {
        public Vector2[] Vertices = new Vector2[4];

        public BoxCollider() { }

        public BoxCollider(Vector2[] vertices)
        {
            if(vertices.Length != 4) { throw new ArgumentException($"Too many vertices in BoxCollider constructor {vertices}"); }
            Vertices = vertices;
        }

        public BoxCollider(BoxCollider other)
        {
            Vertices = other.Vertices;
        }

        public override object Clone()
        {
            return new BoxCollider(this);
        }
    }
}

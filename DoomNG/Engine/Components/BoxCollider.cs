using Microsoft.Xna.Framework;
using System;

namespace DoomNG.Engine.Components
{
    /// <summary>
    /// Stores offsets from transform center
    /// </summary>
    internal class BoxCollider : IComponent
    {
        public BoxCollider() { }

        public BoxCollider(BoxCollider other){}

        public Vector2[] GetVertices(){
            Transform2D t = gameObject.transform;
            if(t == null) return new Vector2[0];

            Vector2 pivot = t.pivot;
            Vector2 position = t.position;
            Vector2 scale = t.scale;
            Vector2 center = position;

            Vector2[] vertices = new Vector2[4];
            vertices[0] = center + new Vector2(-scale.X, -scale.Y)/2;
            vertices[1] = center + new Vector2(scale.X, -scale.Y)/2;
            vertices[2] = center + new Vector2(scale.X, scale.Y)/2;
            vertices[3] = center + new Vector2(-scale.X, scale.Y)/2;

            return vertices;
        }

        public override object Clone()
        {
            return new BoxCollider(this);
        }
    }
}

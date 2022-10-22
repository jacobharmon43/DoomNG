using DoomNG.Engine.Types;
using Microsoft.Xna.Framework;

using System.Linq;
using System.Collections.Generic;

namespace DoomNG.Engine.Components
{
    internal class BoxCollider : Component
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

        public RaycastHit2D? Intersect(Vector2 origin, Vector2 end)
        {
            List<RaycastHit2D> hitPoints = IntersectAll(origin, end).ToList();
            hitPoints = hitPoints.OrderBy(x => x.distance).ToList();
            return hitPoints.Count() > 0 ? hitPoints.First() : null;
        }

        public RaycastHit2D[] IntersectAll(Vector2 origin, Vector2 end)
        {
            Vector2[] vertices = GetVertices();
            Line l = new(origin, end);

            Line[] segments = new Line[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                int endPoint = i + 1;
                if (i + 1 == vertices.Length)
                    endPoint = 0;
                segments[i] = new Line(vertices[i], vertices[endPoint]);
            }

            List<RaycastHit2D> hitPoints = new List<RaycastHit2D>();
            foreach (Line segment in segments)
            {
                Vector2? p = l.Intersects(segment);
                if (p != null)
                {
                    Vector2 normal = new((segment.end.Y - segment.start.Y), (segment.end.X - segment.start.X));
                    float distance = Vector2.Distance(new Vector2(p.Value.X, p.Value.Y), new Vector2(origin.X, origin.Y));
                    hitPoints.Add(new RaycastHit2D(this, distance, normal));
                }
            }
            return hitPoints.ToArray();
        }

        public override object Clone()
        {
            return new BoxCollider(this);
        }
    }
}

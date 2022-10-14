using DoomNG.Engine.Components;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace DoomNG.Engine.Systems
{
    internal class PhysicsSystem
    {
        Scene owner;

        public PhysicsSystem(Scene owner)
        {
            this.owner = owner;
        }


        public RaycastHit2D Raycast(Vector2 start, Vector2 direction, float distance)
        {
            return new RaycastHit2D();
        }

        public RaycastHit2D? Linecast(Vector2 start, Vector2 end)
        {
            foreach(GameObject o in owner.GetObjects())
            {
                BoxCollider collider = o.GetComponent<BoxCollider>();
                if (collider == null) continue;
                return LineIntersects(start, end, collider);
            }
            return new RaycastHit2D();
        }

        bool ccw(Vector2 A, Vector2 B, Vector2 C)
        {
            return (C.Y - A.Y) * (B.X - A.X) > (B.Y - A.Y) * (C.X - A.X);
        }

        bool intersects(Line A, Line B)
        {
            return ccw(A.start, B.start, B.end) != ccw(A.end, B.start, B.end) && ccw(A.start, A.end, B.start) != ccw(A.start, A.end, B.end);
        }

        private static RaycastHit2D? LineIntersects(Vector2 origin, Vector2 end, BoxCollider checkCollision)
        {
            if (checkCollision == null) return null;
            Vector2[] vertices = checkCollision.GetVertices();
            Line l = new(origin, end);

            Line[] segments = new Line[vertices.Length];
            for(int i = 0; i < vertices.Length; i++)
            {
                int endPoint = i + 1;
                if (i + 1 == vertices.Length)
                    endPoint = 0;
                segments[i] = new Line(vertices[i], vertices[endPoint]);
            }

            List<RaycastHit2D> hitPoints = new List<RaycastHit2D>();
            foreach(Line segment in segments)
            {
                Vector2? p = l.Intersects(segment);
                if (p != null)
                {
                    Vector2 normal = new((segment.end.Y - segment.start.Y), (segment.end.X - segment.start.X));
                    float distance = Vector2.Distance(new Vector2(p.Value.X, p.Value.Y), new Vector2(origin.X, origin.Y));
                    hitPoints.Add(new RaycastHit2D (checkCollision, distance, normal));
                }
            }
            hitPoints = hitPoints.OrderBy(x => x.distance).ToList();
            return hitPoints.Count() > 0 ? hitPoints.First() : null;
        }
    }
}

internal struct Line
{
    public Vector2 start;
    public Vector2 end;
    public Line(Vector2 a, Vector2 b){
        start = a; 
        end = b;
    }

    public Vector2? Intersects(Line other)
    {
        float s1_x, s1_y, s2_x, s2_y;
        s1_x = this.end.X - this.start.X;
        s1_y = this.end.Y - this.start.Y;
        s2_x = other.end.X - other.start.X;
        s2_y = other.end.Y - other.start.Y;

        float s_num = -s1_y * (this.start.X - other.start.X) + s1_x * (this.start.Y - other.start.Y);
        float s_den = (-s2_x * s1_y + s1_x * s2_y);
        if (s_den == 0) return null;
        float s = s_num / s_den;

        float t_num = (s2_x * (this.start.Y - other.start.Y) - s2_y * (this.start.X - other.start.X));
        float t_den = (-s2_x * s1_y + s1_x * s2_y);
        if (t_den == 0) return null;
        float t = t_num / t_den;

        Vector2 p = new Vector2();

        if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
        {
            p.X = (int)(this.start.X + (t * s1_x));
            p.Y = (int)(this.start.Y+ (t * s1_y));
            return p;
        }
        return null;
    }
}

internal struct RaycastHit2D{
    public BoxCollider hit;
    public Vector2 normal;
    public float distance;

    public RaycastHit2D(BoxCollider hit, float distance, Vector2 normal)
    {
        this.hit = hit;
        this.distance = distance;
        this.normal = normal;
    }
}

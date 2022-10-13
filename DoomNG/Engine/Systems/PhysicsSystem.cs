using DoomNG.Engine.Components;
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

        public RaycastHit2D Linecast(Vector2 start, Vector2 end, float distance)
        {
            foreach(GameObject o in owner.GetObjects())
            {
                BoxCollider collider = o.GetComponent<BoxCollider>();
                Transform2D transform = o.GetComponent<Transform2D>();
                if (collider == null) continue;

                Line casted = new Line(start, end);
                Vector2[] points = collider.Vertices;
                Line[] lines = new Line[6];
                for(int i = 0; i < 4; i++)
                {
                    for(int j = i+1; j < 4; j++)
                    {
                        lines[i] = new Line(points[i], points[j]);
                    }
                }

                foreach(Line line in lines)
                {
                    if(intersects(casted, line))
                    {
                        Vector2 normal = new Vector2(line.start.Y - line.end.Y, line.end.X - line.start.X);
                        return new RaycastHit2D(collider, normal);
                    }
                }
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
    }

    internal struct Line
    {
        public Vector2 start;
        public Vector2 end;
        public Line(Vector2 a, Vector2 b){
            start = a; 
            end = b;
        }
    }

    internal struct RaycastHit2D{
        public BoxCollider hit;
        public Vector2 normal;

        public RaycastHit2D(BoxCollider hit, Vector2 normal)
        {
            this.hit = hit;
            this.normal = normal;
        }
    };
}

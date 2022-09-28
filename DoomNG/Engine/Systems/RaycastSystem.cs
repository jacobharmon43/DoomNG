using DoomNG.Engine.Components;
using Microsoft.Xna.Framework;
using DoomNG.Engine.Helpers;
using System.Linq;
using System.Collections.Generic;

namespace DoomNG.Engine.Systems
{
    internal class RaycastSystem : ISystem
    {
        EntityManager _entityManager;

        public RaycastSystem(EntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        public RaycastHit? LineCast(Vector2 origin, Vector2 end)
        {
            List<RaycastHit> hits = new List<RaycastHit>();
            List<Entity> haveCollider = _entityManager.GetEntitiesWith<BoxCollider>();
            foreach (Entity entity in haveCollider)
            {
                Collider c = _entityManager.GetComponent<BoxCollider>(entity);
                RaycastHit? r = LineIntersects(origin, end, c);
                if (r != null)
                {
                    hits.Add(r.Value);
                }
            }
            if(hits.Count() == 0)
            {
                return null;
            }
            hits = hits.OrderBy(hit => hit.distance).ToList();
            return hits[0];
        }

        public RaycastHit? LineIntersects(Vector2 origin, Vector2 end, Collider checkCollision)
        {
            if (checkCollision == null) return null;
            Vector2[] vertices = checkCollision.GetVertices();
            Line l = new Line(origin, end);

            Line[] segments = new Line[vertices.Count()];
            for(int i = 0; i < vertices.Count(); i++)
            {
                int endPoint = i + 1;
                if (i + 1 == vertices.Count())
                    endPoint = 0;
                segments[i] = new Line(vertices[i], vertices[endPoint]);
            }

            foreach(Line segment in segments)
            {
                Vector2? p = l.Intersects(segment);
                if (p != null)
                {
                    Vector2 normal = new Vector2((segment.p2.Y - segment.p1.Y), (segment.p2.X - segment.p1.X));
                    float distance = Vector2.Distance(new Vector2(p.Value.X, p.Value.Y), new Vector2(origin.X, origin.Y));
                    return new RaycastHit(p.Value, distance, checkCollision, normal);
                }
            }

            return null;
        }

        public void Execute() { }
    }
    public readonly record struct RaycastHit(Vector2 Vector2, float distance, Collider Collider, Vector2 Normal);
}

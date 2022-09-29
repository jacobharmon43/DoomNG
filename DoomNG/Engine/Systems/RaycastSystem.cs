using DoomNG.Engine.Components;
using Microsoft.Xna.Framework;
using DoomNG.Engine.Helpers;
using System.Linq;
using System.Collections.Generic;

namespace DoomNG.Engine.Systems
{
    internal class RaycastSystem : ISystem
    {
        readonly EntityManager _entityManager;

        public RaycastSystem(EntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        public RaycastHit? LineCast(Vector2 origin, Vector2 end)
        {
            List<RaycastHit> hits = new();
            List<Entity> haveCollider = _entityManager.GetEntitiesWith<BoxCollider>();
            foreach (Entity entity in haveCollider)
            {
                Collider c = _entityManager.GetComponent<BoxCollider>(entity);
                RaycastHit? r = LineIntersects(origin, end, c, entity);
                if (r != null)
                {
                    hits.Add(r.Value);
                }
            }
            if(hits.Count == 0)
            {
                return null;
            }
            hits = hits.OrderBy(hit => hit.Distance).ToList();
            return hits[0];
        }

        private static RaycastHit? LineIntersects(Vector2 origin, Vector2 end, Collider checkCollision, Entity entity)
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

            List<RaycastHit> hitPoints = new List<RaycastHit>();
            foreach(Line segment in segments)
            {
                Vector2? p = l.Intersects(segment);
                if (p != null)
                {
                    Vector2 normal = new((segment.p2.Y - segment.p1.Y), (segment.p2.X - segment.p1.X));
                    float distance = Vector2.Distance(new Vector2(p.Value.X, p.Value.Y), new Vector2(origin.X, origin.Y));
                    hitPoints.Add(new RaycastHit(p.Value, distance, entity, checkCollision, normal));
                }
            }
            hitPoints = hitPoints.OrderBy(x => x.Distance).ToList();
            return hitPoints.Count() > 0 ? hitPoints.First() : null;
        }

        public void Execute(GameTime gameTime) { }
    }
    internal record struct RaycastHit(Vector2 point, float Distance, Entity Entity, Collider Collider, Vector2 Normal);
}

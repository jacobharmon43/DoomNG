using DoomNG.Engine.Components;
using DoomNG.Engine.Types;

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

        public RaycastHit2D? Linecast(Vector2 start, Vector2 end, ContactFilter? filter = null)
        {
            List<RaycastHit2D> list = LinecastAll(start, end, filter).ToList();
            if (list.Count == 0) return null;
            list.OrderBy(x => x.distance);
            return list[0];
        }

        public RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, ContactFilter? filter = null)
        {
            List<RaycastHit2D> hitObjects = new List<RaycastHit2D>();
            foreach (GameObject o in owner.GetObjects())
            {
                if (filter.HasValue)
                {
                    switch (filter.Value.FilterType)
                    {
                        case FilterType.WhiteList:
                            if (!filter.Value.filteredObjects.Contains(o))
                                continue;
                            break;
                        case FilterType.BlackList:
                            if (filter.Value.filteredObjects.Contains(o))
                                continue;
                            break;
                        default:
                            break;
                    }
                }
                BoxCollider collider = o.GetComponent<BoxCollider>();
                if (collider == null) continue;
                RaycastHit2D? hit = collider.Intersect(start, end);
                if (!hit.HasValue) continue;
                hitObjects.Add(hit.Value);
            }
            return hitObjects.ToArray();
        }
    }
}

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using DoomNG.Engine.Components;

namespace DoomNG.Engine.Systems
{
    internal class ColliderSystem : ISystem
    {
        EntityManager _entityManager;

        public ColliderSystem(EntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        public void Execute(GameTime gameTime)
        {
            List<Entity> entities = _entityManager.GetEntitiesWith<BoxCollider>();
            foreach(Entity entity in entities)
            {
                BoxCollider b = _entityManager.GetComponent<BoxCollider>(entity);
                Transform2D t = _entityManager.GetComponent<Transform2D>(entity);
                Pivot p = _entityManager.HasComponent<Pivot>(entity) ? _entityManager.GetComponent<Pivot>(entity) : new Pivot(0,0);

                b.UpdatePosition(t, p);
            }
        }
    }
}

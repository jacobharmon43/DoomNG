using System;
using System.Collections.Generic;

namespace DoomNG.Engine{
    internal interface IEntityManager{
        public Entity CreateEntity(params IComponent[] components);
        public void SetComponent<T>(Entity entity, T component) where T : IComponent;
        public List<Entity> GetEntitiesWith<T>() where T : IComponent;
        public List<Entity> GetEntitiesWith(params Type[] types);
        public T GetComponent<T>(Entity entity) where T : IComponent;
        public bool HasComponent<T>(Entity entity) where T : IComponent;
    }
}
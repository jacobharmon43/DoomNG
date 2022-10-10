using System;
using System.Collections.Generic;

namespace DoomNG.Engine{
    internal interface IEntityManager : ISystem{

        public Entity CreateEntity(params IComponent[] components);
        public void RemoveEntity(Entity entity);
        public void SetComponent<T>(Entity entity, T component) where T : IComponent;
        public void RemoveComponent<T>(Entity entity) where T : IComponent;
        public T GetComponent<T>(Entity entity) where T : IComponent;
        public bool HasComponent<T>(Entity entity) where T : IComponent;

        public List<Entity> GetEntitiesWith<T>() where T : IComponent;
        public List<Entity> GetEntitiesWith(params Type[] types);
    }
}
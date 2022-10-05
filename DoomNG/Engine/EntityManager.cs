using System;
using System.Linq;
using System.Collections.Generic;

namespace DoomNG.Engine
{
    internal class EntityManager : IEntityManager
    {
        readonly List<Entity> _entities;
        readonly Dictionary<Type, List<Entity>> _entitiesByType;
        readonly Dictionary<Entity, List<IComponent>> _components;
        int _nextId = 0;

        public EntityManager()
        {
            _entities = new List<Entity>();
            _entitiesByType = new Dictionary<Type, List<Entity>>();
            _components = new Dictionary<Entity, List<IComponent>>();
        }

        public Entity CreateEntity(params IComponent[] components)
        {
            var entity = new Entity(_nextId++);
            _entities.Add(entity);
            foreach(IComponent component in components)
            {
                SetComponent(entity, component);
            }
            return entity;
        }

        public void RemoveEntity(Entity e)
        {
            foreach(IComponent component in _components[e])
            {
                _entitiesByType[component.GetType()].Remove(e);
            }
            _entities.Remove(e);
        }

        public void SetComponent<T>(Entity entity, T component) where T : IComponent
        {
            Type type = component.GetType();

            if (!_components.ContainsKey(entity))
                _components.Add(entity, new List<IComponent>());
            if (!_entitiesByType.ContainsKey(type))
                _entitiesByType.Add(type, new List<Entity>());

            _components[entity].Add(component);
            _entitiesByType[type].Add(entity);
        }

        public List<Entity> GetEntitiesWith<T>() where T : IComponent
        {
            return _entitiesByType[typeof(T)];
        }

        public List<Entity> GetEntitiesWith(params Type[] types)
        {
            List<Entity> retList = new();
            foreach(Type t in types)
            {
                if(retList.Count == 0)
                {
                    retList = _entitiesByType[t];
                }
                else
                {
                    retList = retList.Intersect(_entitiesByType[t]).ToList();
                }
            }
            return retList;
        }

        public T[] GetComponents<T>() where T : IComponent
        {
            List<T> retList = new();
            foreach(Entity e in _entitiesByType[typeof(T)])
            {
                retList.Add(GetComponent<T>(e));
            }
            return retList.ToArray();
        }

        public T GetComponent<T>(Entity entity) where T : IComponent
        {
            foreach(var component in _components[entity])
            {
                if(component.GetType() == typeof(T))
                {
                    return (T)component;
                }
            }
            return default;
        }

        public bool HasComponent<T>(Entity entity) where T : IComponent
        {
            if (!_entitiesByType.ContainsKey(typeof(T))) return false;
            return _entitiesByType[typeof(T)].Contains(entity);
        }
    }
}

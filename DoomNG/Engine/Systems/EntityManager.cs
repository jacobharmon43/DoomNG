using System;
using System.Collections.Generic;

namespace DoomNG.Engine.Systems
{
    /// <summary>
    /// Storage and delivery system for entities in the game
    /// </summary>
    /// <seealso cref="DoomNG.Engine.ISystem" />
    internal class EntityManager : ISystem
    {
        readonly List<Entity> _entities;
        readonly Dictionary<Type, List<Entity>> _entitiesByType;
        Dictionary<Entity, List<IComponent>> _components;
        int _nextId = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityManager"/> class.
        /// </summary>
        public EntityManager()
        {
            _entities = new List<Entity>();
            _entitiesByType = new Dictionary<Type, List<Entity>>();
            _components = new Dictionary<Entity, List<IComponent>>();
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public void Execute() { }

        /// <summary>
        /// Creates the entity, attaches desired components
        /// </summary>
        /// <param name="components"> The components. </param>
        /// <returns> The Entity. </returns>
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

        /// <summary>
        /// Sets the dictionaries for a entity to note the existence of, and the reference to its component
        /// </summary>
        /// <typeparam name="T"> The type of the component </typeparam>
        /// <param name="entity"> The entity.</param>
        /// <param name="component"> The component.</param>
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

        /// <summary>
        /// Gets the entities with desired Component
        /// </summary>
        /// <typeparam name="T"> Type of desired component </typeparam>
        /// <returns> A List of Entities containing that component </returns>
        public List<Entity> GetEntitiesWith<T>() where T : IComponent
        {
            return _entitiesByType[typeof(T)];
        }

        /// <summary>
        /// Gets the component on an entity if it exists
        /// </summary>
        /// <typeparam name="T"> Type of desired component </typeparam>
        /// <param name="entity"> The entity.</param>
        /// <returns> The component desired, null otherwise </returns>
        public T GetComponent<T>(Entity entity) where T : IComponent
        {
            foreach(var component in _components[entity])
            {
                if(component.GetType() == typeof(T))
                {
                    return (T)component;
                }
            }
            return default(T);
        }

        /// <summary>
        /// Checks if a entity is holding a component of said type
        /// </summary>
        /// <typeparam name="T"> Type of desired component </typeparam>
        /// <param name="entity"> The entity.</param>
        /// <returns> True if the entity contains a component of that type, false otherwise </returns>
        public bool HasComponent<T>(Entity entity) where T : IComponent
        {
            if (!_entitiesByType.ContainsKey(typeof(T))) return false;
            return _entitiesByType[typeof(T)].Contains(entity);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace DoomNG.Engine
{
    internal class GameObject : ICloneable
    {
        public Scene ownerScene;
        Dictionary<Type, IComponent> _components;

        //Constructors
        public GameObject(){
            _components = new Dictionary<Type, IComponent>();
        }

        public GameObject(params IComponent[] components)
        {
            _components = new Dictionary<Type, IComponent>();
            foreach (IComponent component in components)
            {
                AddComponent(component);
            }
        }

        public GameObject(GameObject other)
        {
            _components = new Dictionary<Type, IComponent>();
            foreach (IComponent component in other.GetComponents())
            {
                AddComponent(component);
            }
        }

        //Methods
        public void AddComponent<T>(T component) where T : IComponent
        {
            if(_components.ContainsKey(component.GetType())) throw new Exception($"Key already exists in dictionary for type {component.GetType().Name}");
            IComponent componentToAdd = (T)component.Clone();
            _components.Add(component.GetType(), componentToAdd);
            componentToAdd.gameObject = this;
        }

        public bool TryAddComponent<T>(T component) where T : IComponent
        {
            if (_components.ContainsKey(component.GetType())) return false;
            AddComponent(component);
            return true;
        }

        public void RemoveComponent<T>() where T : IComponent
        {
            if (!_components.ContainsKey(typeof(T))) return;
            _components[typeof(T)].OnDestroy();
            _components.Remove(typeof(T));
        }

        public T GetComponent<T>() where T : IComponent
        {
            if(_components.ContainsKey(typeof(T)))
                return (T)_components[typeof(T)];
            return null;
        }

        public IComponent[] GetComponents()
        {
            return _components.Values.ToArray();
        }

        //Interface implements
        public object Clone()
        {
            return new GameObject(this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

using DoomNG.Engine.Components;

namespace DoomNG.Engine
{
    internal class GameObject : ICloneable
    {
        public Scene OwnerScene;
        Dictionary<Type, Component> _components;

        //Constructors
        public GameObject(){
            _components = new Dictionary<Type, Component>();
        }

        public GameObject(params Component[] components)
        {
            _components = new Dictionary<Type, Component>();
            foreach (Component component in components)
            {
                AddComponent(component);
            }
        }

        public GameObject(GameObject other)
        {
            _components = new Dictionary<Type, Component>();
            foreach (Component component in other.GetComponents())
            {
                AddComponent(component);
            }
        }

        //Methods
        public void AddComponent<T>(T component) where T : Component
        {
            Type type = component.GetType();
            if(_components.ContainsKey(type)) throw new Exception($"Key already exists in dictionary for type {component.GetType().Name}");
            Component componentToAdd = (T)component.Clone();
            _components.Add(component.GetType(), componentToAdd);
            componentToAdd.gameObject = this;
        }

        public bool TryAddComponent<T>(T component) where T : Component
        {
            if (_components.ContainsKey(component.GetType())) return false;
            AddComponent(component);
            return true;
        }

        public void RemoveComponent<T>() where T : Component
        {
            if (!_components.ContainsKey(typeof(T))) return;
            _components[typeof(T)].OnDestroy();
            _components.Remove(typeof(T));
        }

        public T GetComponent<T>() where T : Component
        {
            if(_components.ContainsKey(typeof(T)))
                return (T)_components[typeof(T)];
            return null;
        }

        public Component[] GetComponents()
        {
            return _components.Values.ToArray();
        }

        //Interface implements
        public object Clone()
        {
            return new GameObject(this);
        }

        public Transform2D transform => GetComponent<Transform2D>();
    }
}

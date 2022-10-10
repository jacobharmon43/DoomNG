using System.Collections.Generic;

namespace DoomNG.Engine.Helpers
{
    internal class EntityBuilder
    {
        List<IComponent> _components;
        
        public EntityBuilder()
        {
            _components = new List<IComponent>();
        }

        public EntityBuilder WithComponent(IComponent c)
        {
            _components.Add(c);
            return this;
        }

        public IComponent[] Build() { return _components.ToArray(); }
    }
}

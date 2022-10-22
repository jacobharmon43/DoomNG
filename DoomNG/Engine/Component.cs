using System;

namespace DoomNG.Engine
{
    internal abstract class Component : ICloneable
    {
        public GameObject gameObject;
        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void PhysicsUpdate() { }
        public virtual void OnDestroy() { }

        public abstract object Clone();
    }
}

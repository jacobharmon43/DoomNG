using Microsoft.Xna.Framework;

namespace DoomNG.Engine.Components
{
    internal class PhysicsBody : IComponent
    {
        public Vector2 Velocity;
        public Vector2 AngularVelocity;

        public enum ForceMode { Impulse }

        public PhysicsBody()
        {

        }

        void AddForce(Vector2 force, ForceMode mode, float deltaTime)
        {
            switch (mode)
            {
                case ForceMode.Impulse:
                    Velocity += force * deltaTime;
                    break;
                default:
                    break;
            }
        }
    }
}

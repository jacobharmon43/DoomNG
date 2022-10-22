using DoomNG.Engine.Components;
using Microsoft.Xna.Framework;

namespace DoomNG.Engine.Types
{

    internal struct RaycastHit2D
    {
        public BoxCollider hit;
        public Vector2 normal;
        public float distance;

        public RaycastHit2D(BoxCollider hit, float distance, Vector2 normal)
        {
            this.hit = hit;
            this.distance = distance;
            this.normal = normal;
        }
    }
}

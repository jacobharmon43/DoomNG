using DoomNG.Engine;
using Microsoft.Xna.Framework;

namespace DoomNG.DoomSpire.Components
{    
    internal class Player : IComponent
    {
        public enum State { Idle, Moving };
        public State current = State.Idle;

        public Point desiredPoint;
        public bool everSet = false;

        public Player() {
            
        }

        public void SetDesiredPoint(Point set)
        {
            this.desiredPoint = set;
            this.everSet = true;
        }
    }
}

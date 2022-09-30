using Microsoft.Xna.Framework;
using System;

namespace DoomNG.Engine.Components
{
    /// <summary>
    /// Stores placement and size info for entities
    /// </summary>
    /// <seealso cref="DoomNG.Engine.IComponent" />
    public class Transform2D : IComponent
    {
        public Vector2 position;
        public Vector2 scale;
        public float rotation;

        /// <summary>
        /// Initializes a new instance of the <see cref="Transform2D"/> class.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="rot">The rot.</param>
        public Transform2D(Vector2 pos, Vector2 scale, float rot)
        {
            this.position = pos;
            this.scale = scale;
            this.rotation = rot;
        }

        /// <summary>
        /// Translates the position by the  specified movement.
        /// </summary>
        /// <param name="movement">The movement.</param>
        public void Translate(Vector2 movement)
        {
            this.position += movement;
        }

        public void Translate(int x, int y)
        {
            this.position += new Vector2(x,y);
        }

        public Vector2 forward => GetForward();

        private Vector2 GetForward(){
            rotation = ClampAngle(rotation);
            return new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
        }

        public float ClampAngle(float angle){
            while(angle >= 360){
                angle -= 360;
            }
            if(angle < 0){
                angle += 360;
            }
            return angle;
        }
    }
}

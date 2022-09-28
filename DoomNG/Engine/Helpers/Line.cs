using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoomNG.Engine.Helpers
{
    internal class Line
    {
        public Vector2 p1;
        public Vector2 p2;

        public Line(Vector2 p1, Vector2 p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        public Vector2? Intersects(Line other)
        {
            float s1_x, s1_y, s2_x, s2_y;
            s1_x = this.p2.X - this.p1.X;
            s1_y = this.p2.Y - this.p1.Y;
            s2_x = other.p2.X - other.p1.X;
            s2_y = other.p2.Y - other.p1.Y;

            float s_num = -s1_y * (this.p1.X - other.p1.X) + s1_x * (this.p1.Y - other.p1.Y);
            float s_den = (-s2_x * s1_y + s1_x * s2_y);
            if (s_den == 0) return null;
            float s = s_num / s_den;

            float t_num = (s2_x * (this.p1.Y - other.p1.Y) - s2_y * (this.p1.X - other.p1.X));
            float t_den = (-s2_x * s1_y + s1_x * s2_y);
            if (t_den == 0) return null;
            float t = t_num / t_den;

            Vector2 p = new Vector2();

            if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
            {
                p.X = (int)(this.p1.X + (t * s1_x));
                p.Y = (int)(this.p1.Y+ (t * s1_y));
                return p;
            }
            return null;
        }
    }
}

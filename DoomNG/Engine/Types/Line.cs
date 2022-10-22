using Microsoft.Xna.Framework;

namespace DoomNG.Engine.Types
{
    internal struct Line
    {
        public Vector2 start;
        public Vector2 end;
        public Line(Vector2 a, Vector2 b)
        {
            start = a;
            end = b;
        }

        public Vector2? Intersects(Line other)
        {
            float s1_x, s1_y, s2_x, s2_y;
            s1_x = this.end.X - this.start.X;
            s1_y = this.end.Y - this.start.Y;
            s2_x = other.end.X - other.start.X;
            s2_y = other.end.Y - other.start.Y;

            float s_num = -s1_y * (this.start.X - other.start.X) + s1_x * (this.start.Y - other.start.Y);
            float s_den = (-s2_x * s1_y + s1_x * s2_y);
            if (s_den == 0) return null;
            float s = s_num / s_den;

            float t_num = (s2_x * (this.start.Y - other.start.Y) - s2_y * (this.start.X - other.start.X));
            float t_den = (-s2_x * s1_y + s1_x * s2_y);
            if (t_den == 0) return null;
            float t = t_num / t_den;

            Vector2 p = new Vector2();

            if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
            {
                p.X = (int)(this.start.X + (t * s1_x));
                p.Y = (int)(this.start.Y + (t * s1_y));
                return p;
            }
            return null;
        }
    }
}

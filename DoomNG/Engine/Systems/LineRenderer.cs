using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DoomNG.Engine.Systems
{
    internal class LineRenderer
    {
        List<Tuple<Point, Point>> _linesToDrawThisFrame = new();

        Texture2D pixel = null;

        public LineRenderer(Texture2D t)
        {
            pixel = t;
        }

        public void AddLineToFrame(Point a, Point b)
        {
            _linesToDrawThisFrame.Add(Tuple.Create<Point, Point>(a, b));
        }

        public void RenderLines(SpriteBatch _batch)
        {
            foreach(Tuple<Point, Point> p in _linesToDrawThisFrame)
            {
                Vector2 start = new Vector2(p.Item1.X, p.Item1.Y);
                Vector2 end = new Vector2(p.Item2.X, p.Item2.Y);
                _batch.Draw(pixel,
                    start,
                    null,
                    Color.White, 
                    (float)Math.Atan2(p.Item2.Y - p.Item1.Y, p.Item2.X - p.Item1.X), 
                    Vector2.Zero,
                    new Vector2(Vector2.Distance(start,end), 1f),
                    SpriteEffects.None, 
                    0);
            }
            _linesToDrawThisFrame.Clear();
        }

        public int LinesToDraw => _linesToDrawThisFrame.Count;
    }
}

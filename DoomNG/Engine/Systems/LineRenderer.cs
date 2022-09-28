using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DoomNG.Engine.Systems
{
    internal class LineRenderer
    {
        List<Tuple<Vector2, Vector2, Color>> _linesToDrawThisFrame = new();

        Texture2D pixel = null;

        public LineRenderer(Texture2D t)
        {
            pixel = t;
        }

        public void AddLineToFrame(Vector2 a, Vector2 b, Color? c = null)
        {
            _linesToDrawThisFrame.Add(Tuple.Create<Vector2, Vector2, Color>(a, b, c != null ? c.Value : Color.White)) ;
        }

        public void RenderLines(SpriteBatch _batch)
        {
            foreach(Tuple<Vector2, Vector2, Color> p in _linesToDrawThisFrame)
            {
                Vector2 start = p.Item1;
                Vector2 end = p.Item2;
                _batch.Draw(pixel,
                    start,
                    null,
                    p.Item3, 
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

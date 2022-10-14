using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace DoomNG.Engine.Systems{
    internal static class Gizmos{
        static List<Tuple<Line, Color>> _renderLines = new List<Tuple<Line,Color>>();

        public static void AddLineToRender(Line l, Color c){
            _renderLines.Add(new Tuple<Line, Color>(l,c));
        }

        public static void Render(SpriteBatch batch){
            Texture2D pixel = TextureDistributor.GetTexture("Pixel");
            foreach(var l in _renderLines)
            {
                Vector2 start = l.Item1.start;
                Vector2 end = l.Item1.end;
                Color c = l.Item2;
                batch.Draw(pixel,
                    start,
                    null,
                    c, 
                    (float)Math.Atan2(end.Y- start.Y, end.X - start.X), 
                    Vector2.Zero,
                    new Vector2(Vector2.Distance(start,end), 1f),
                    SpriteEffects.None, 
                    0);
            }
            _renderLines.Clear();
        }
    }
}
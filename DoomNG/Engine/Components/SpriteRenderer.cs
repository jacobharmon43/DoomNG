using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DoomNG.Engine.Components
{
    internal class SpriteRenderer : Component
    {
        public Texture2D Texture;
        public Color Color;
        public int Layer;
        public int ZIndex;

        public SpriteRenderer()
        {
            Texture = null;
            Color = Color.White;
            Layer = 0;
            ZIndex = 0;
        }

        public SpriteRenderer(Texture2D texture, Color color, int layer = 0, int zIndex = 0)
        {
            Texture = texture;
            Color = color;
            Layer = layer;
            ZIndex = zIndex;
        }

        public SpriteRenderer(SpriteRenderer other)
        {
            Texture = other.Texture;
            Color = other.Color;
            Layer = other.Layer;
            ZIndex = other.ZIndex;
        }

        public override object Clone()
        {
            return new SpriteRenderer(this);
        }
    }
}

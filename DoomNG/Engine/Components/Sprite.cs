using Microsoft.Xna.Framework.Graphics;
using DoomNG.Engine.Systems;

namespace DoomNG.Engine.Components
{
    /// <summary>
    /// Storage class for textures to be rendered by the <see cref="SpriteRenderSystem"/>
    /// </summary>
    /// <seealso cref="DoomNG.Engine.IComponent" />
    public class Sprite : IComponent
    {
        public Texture2D texture;

        public Sprite(Texture2D text) => this.texture = text;
    }
}

using Microsoft.Xna.Framework.Graphics;
using System;

namespace DoomNG.Engine
{
    internal interface IRenderSystem : ISystem
    {
        public void Render(SpriteBatch batch);
    }
}

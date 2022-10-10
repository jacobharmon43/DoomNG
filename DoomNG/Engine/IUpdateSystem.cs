using Microsoft.Xna.Framework;
using System;

namespace DoomNG.Engine
{
    internal interface IUpdateSystem : ISystem{
        public void Execute(GameTime gameTime);
    }
}

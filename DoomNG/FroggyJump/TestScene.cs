using DoomNG.Engine;
using DoomNG.Engine.Components;

using DoomNG.FroggyJump.Components;
using Microsoft.Xna.Framework;

namespace DoomNG.FroggyJump
{
    internal class TestScene : Scene
    {
        public override void Initialize()
        {
            base.Initialize();
            AddObject(
                new GameObject(
                    new PlayerController(),
                    new Transform2D(new Vector2(250, 250), new Vector2(64, 64), 0, new Vector2(0.5f,0.5f)),
                    new SpriteRenderer(TextureDistributor.GetTexture("Pixel"), Color.White)
                )
            );
        }
    }
}

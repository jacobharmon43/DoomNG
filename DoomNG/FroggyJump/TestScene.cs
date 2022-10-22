using DoomNG.Engine;
using DoomNG.Engine.Systems;
using DoomNG.Engine.Components;

using DoomNG.FroggyJump.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DoomNG.FroggyJump
{
    internal class TestScene : Scene
    {

        GameObject player;
        public override void Initialize()
        {
            base.Initialize();
            player = AddObject(
                new GameObject(
                    new PlayerController(),
                    new Transform2D(new Vector2(250, 250), new Vector2(64, 64), 0, new Vector2(0.5f,0.5f)),
                    new SpriteRenderer(TextureDistributor.GetTexture("Pixel"), Color.White),
                    new BoxCollider()
                )
            );

            /*AddObject(
                new GameObject(
                    new SpriteRenderer(TextureDistributor.GetTexture("Pixel"), Color.White),
                    new Transform2D(new Vector2(400,250), new Vector2(64,64), 0, new Vector2(0.5f, 0.5f)),
                    new BoxCollider()
                )
            );*/
        }

        public override void Update()
        {
            base.Update();
            Gizmos.DrawBox(player.GetComponent<BoxCollider>().GetVertices());
        }

        public override void Render(SpriteBatch batch)
        {
            base.Render(batch);
            Gizmos.Render(batch);
        }
    }
}

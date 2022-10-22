using DoomNG.Engine;
using DoomNG.Engine.Systems;
using DoomNG.FroggyJump;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DoomNG
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        TestScene scene;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            scene = new TestScene();
            scene.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D pixel = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            TextureDistributor.AddTexture("Pixel", pixel);
        }

        protected override void Update(GameTime gameTime)
        {
            Time.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!base.IsActive) return;
            scene.Update();
            KeyboardQuery.UpdateKeyboard();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            scene.Render(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
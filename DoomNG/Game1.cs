using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using DoomNG.Engine;
using DoomNG.Engine.Components;
using DoomNG.Engine.Systems;
using DoomNG.DoomSpire.Components;
using DoomNG.DoomSpire.Systems;

using System;

namespace DoomNG
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private EntityManager _entityManager;
        private PlayerSystem _playerSystem;
        private SpriteRenderSystem _renderSystem;
        private LineRenderer _lineRenderer;

        private Texture2D _tmp;
        private Texture2D _pixel;
        string _debugText = "Wow this engine is great!";
        SpriteFont _font;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _entityManager = new EntityManager();
            _renderSystem = new SpriteRenderSystem(_entityManager, _spriteBatch);
            _lineRenderer = new LineRenderer(_pixel);
            _playerSystem = new PlayerSystem(_entityManager, _lineRenderer, GraphicsDevice);
            Vector2 screenSize = new Vector2(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);
            Vector2 screenCenter = screenSize / 2;

            _entityManager.CreateEntity(new Sprite(_tmp), new Transform2D(new Point((int)screenCenter.X, (int)screenCenter.Y), new Point(64, 64), 0), new Player(), new Pivot(0.5f, 0.5f), new SpriteLayer(1, 1));
            _entityManager.CreateEntity(new Sprite(_tmp));
        }

        protected override void LoadContent()
        {
            _tmp = Content.Load<Texture2D>("Angered");
            _pixel = new Texture2D(_graphics.GraphicsDevice, 1,1);
            _pixel.SetData(new Color[] { Color.White });
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("PixelFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (!base.IsActive) return;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _playerSystem.Execute();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, _debugText, new Vector2(GraphicsDevice.PresentationParameters.BackBufferWidth / 2, 10), Color.White);
            _lineRenderer.RenderLines(_spriteBatch);
            _renderSystem.Render(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
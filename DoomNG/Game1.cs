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
        private RaycastSystem _raycastSystem;

        private Texture2D _tmp;
        private Texture2D _pixel;
        private Texture2D _redSquare;
        private Texture2D _brown;
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
            _raycastSystem = new RaycastSystem(_entityManager);
            _playerSystem = new PlayerSystem(_entityManager, _lineRenderer, GraphicsDevice, _raycastSystem);

            Vector2 screenSize = new Vector2(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);
            Vector2 screenCenter = screenSize / 2;
            Vector2 halfPivot = new Vector2(0.5f, 0.5f);

            _entityManager.CreateEntity(new Sprite(_redSquare), new Transform2D(Vector2.Zero, new Vector2(64, 64), 0), new Player(), new Pivot(halfPivot), new SpriteLayer(1, 1));
            _entityManager.CreateEntity(new Sprite(_pixel), new Transform2D(screenCenter, new Vector2(64,64), 0), new BoxCollider(screenCenter, new Vector2(64,64), halfPivot), new SpriteLayer(1,0), new Pivot(0.5f, 0.5f));
            _entityManager.CreateEntity(new Sprite(_pixel), new Transform2D(screenCenter - new Vector2(256,256), new Vector2(64, 64), 0), new BoxCollider(screenCenter - new Vector2(256, 256), new Vector2(64, 64), halfPivot), new SpriteLayer(1, 0), new Pivot(0.5f, 0.5f));
            _entityManager.CreateEntity(new Sprite(_pixel), new Transform2D(screenCenter + new Vector2(256,256), new Vector2(64, 64), 0), new BoxCollider(screenCenter + new Vector2(256, 256), new Vector2(64, 64), halfPivot), new SpriteLayer(1, 0), new Pivot(0.5f, 0.5f));
            _entityManager.CreateEntity(new Sprite(_brown), new Transform2D(screenCenter, new Vector2(1024, 1024), 0), new SpriteLayer(0,0), new Pivot(halfPivot));

        }

        protected override void LoadContent()
        {
            _tmp = Content.Load<Texture2D>("Angered");

            _brown = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            _brown.SetData(new Color[] { Color.Brown });

            _redSquare = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            _redSquare.SetData(new Color[] { Color.Red });

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
            _playerSystem.Execute(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _renderSystem.Render(_spriteBatch);
            _lineRenderer.RenderLines(_spriteBatch);
            _spriteBatch.DrawString(_font, _debugText, new Vector2(GraphicsDevice.PresentationParameters.BackBufferWidth / 2, 10), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
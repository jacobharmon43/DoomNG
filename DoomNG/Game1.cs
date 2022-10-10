using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using DoomNG.Engine;
using DoomNG.Engine.Components;
using DoomNG.Engine.Systems;
using DoomNG.DoomSpire.Systems;

using DoomNG.Engine.Helpers;
using System;

using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace DoomNG
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private EntityManager _entityManager;
        private SystemDistributor _distributor;
        private PlayerSystem _playerSystem;

        private Texture2D _tmp;
        private Texture2D _pixel;
        private Texture2D _redSquare;
        private Texture2D _brown;
        private SpriteFont _font;

        private Camera _cam;

        TiledMap _tiledMap;
        TiledMapRenderer _tiledMapRenderer;


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
            _distributor = new SystemDistributor(_entityManager);
            _distributor.AddRenderSystem<LineRenderer>();
            _distributor.AddRenderSystem<SpriteRenderSystem>();
            _distributor.AddUpdateSystem<RaycastSystem>();
            _distributor.AddUpdateSystem<ColliderSystem>();
            _playerSystem = _distributor.AddUpdateSystem<PlayerSystem>();

            Vector2 screenSize = new Vector2(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);
            Vector2 screenCenter = screenSize / 2;
            Vector2 halfPivot = new Vector2(0.5f, 0.5f);

            int TerrainLayer = Layers.GetLayer(Layer.Terrain);
            int BackgroundLayer = Layers.GetLayer(Layer.Background);
            _entityManager.CreateEntity(new Sprite(_pixel), new Transform2D(new Vector2(screenCenter.X, screenSize.Y), new Vector2(screenSize.X,64), 0), new BoxCollider(), new SpriteLayer(TerrainLayer,0), new Pivot(0.5f, 0.5f));
            _entityManager.CreateEntity(new Sprite(_pixel), new Transform2D(new Vector2(screenSize.X, screenCenter.Y), new Vector2(64, screenSize.Y), 0), new BoxCollider(), new SpriteLayer(BackgroundLayer, 0), new Pivot(0.5f, 0.5f));
            _entityManager.CreateEntity(new Sprite(_brown), new Transform2D(screenCenter, new Vector2(1024, 1024), 0), new SpriteLayer(0,0), new Pivot(halfPivot));
            Entity e = _entityManager.CreateEntity(new Camera(new Vector3(screenCenter.X, screenCenter.Y, 0)));

            _playerSystem.CreatePlayer();

            _cam = _entityManager.GetComponent<Camera>(e);

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

            TextureDistributor.AddTexture("Brown", _brown);
            TextureDistributor.AddTexture("Player", _redSquare);
            TextureDistributor.AddTexture("White", _pixel);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("PixelFont");

            _tiledMap = Content.Load<TiledMap>("TestMap");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
        }

        protected override void Update(GameTime gameTime)
        {
            if (!base.IsActive) return;
            KeyboardQuery.UpdateKeyboard();
            _distributor.Execute(gameTime);
            _tiledMapRenderer.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(transformMatrix: _cam.Transform);
            _distributor.Render(_spriteBatch);
            _tiledMapRenderer.Draw();
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
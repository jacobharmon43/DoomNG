using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using DoomNG.DoomSpire.Systems;
using DoomNG.Engine.Systems;

namespace DoomNG.Engine
{
    internal class SystemDistributor : IServiceProvider{

        EntityManager _entityManager;
        Dictionary<Type, IRenderSystem> _renderSystems;
        Dictionary<Type, IUpdateSystem> _updateSystems;

        Dictionary<Type, Func<ISystem>> _constructors = new Dictionary<Type, Func<ISystem>>();
        

        public SystemDistributor(EntityManager entityManager){
            this._entityManager = entityManager;
            this._renderSystems = new Dictionary<Type, IRenderSystem>();
            this._updateSystems = new Dictionary<Type, IUpdateSystem>();

            _constructors.Add(typeof(PlayerSystem), ConstructPlayerSystem);
            _constructors.Add(typeof(SpriteRenderSystem), ConstructSpriteRenderer);
            _constructors.Add(typeof(LineRenderer), ConstructLineRenderer);
            _constructors.Add(typeof(ColliderSystem), ConstructColliderSystem);
            _constructors.Add(typeof(RaycastSystem), ConstructRaycastSystem);
        }
        
        public T AddUpdateSystem<T>() where T : IUpdateSystem
        {
            T instance = (T)_constructors[typeof(T)]?.Invoke();
            _updateSystems.Add(typeof(T), instance);
            return instance;
        }

        public T AddRenderSystem<T>() where T : IRenderSystem
        {
            T instance = (T)_constructors[typeof(T)]?.Invoke();
            _renderSystems.Add(typeof(T), instance);
            return instance;
        }

        public PlayerSystem ConstructPlayerSystem() => new PlayerSystem(_entityManager, (RaycastSystem)_updateSystems[typeof(RaycastSystem)]);
        public SpriteRenderSystem ConstructSpriteRenderer() => new SpriteRenderSystem(_entityManager);
        public LineRenderer ConstructLineRenderer() => new LineRenderer();
        public ColliderSystem ConstructColliderSystem() => new ColliderSystem(_entityManager);
        public RaycastSystem ConstructRaycastSystem() => new RaycastSystem(_entityManager);


        public void Execute(GameTime gameTime)
        {
            foreach(var system in _updateSystems)
            {
                system.Value.Execute(gameTime);
            }
        }

        public void Render(SpriteBatch batch)
        {
            foreach (var system in _renderSystems)
            {
                system.Value.Render(batch);
            }
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}
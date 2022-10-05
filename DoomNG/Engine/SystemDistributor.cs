using DoomNG.Engine.Systems;
using System.Collections.Generic;

namespace DoomNG.Engine
{
    internal class SystemDistributor{

        EntityManager _entityManager;
        List<ISystem> _renderSystems;
        List<ISystem> _updateSystems;

        public SystemDistributor(EntityManager entityManager){
            this._entityManager = entityManager;
            this._renderSystems = new List<ISystem>();
            this._updateSystems = new List<ISystem>();
        }
        
        public void AddUpdateSystem<T>() where T : ISystem{
            _updateSystems.Add(Construct<T>());
        }

        public void AddRenderSystem<T>() where T : ISystem{
            _renderSystems.Add(Construct<T>());
        }

        public T Construct<T>() where T : ISystem{
            return default(T);
        }
    }
}
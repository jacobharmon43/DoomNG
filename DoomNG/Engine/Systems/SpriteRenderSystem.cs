using DoomNG.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace DoomNG.Engine.Systems
{
    internal class SpriteRenderSystem : IRenderSystem
    {
        readonly EntityManager _entityManager;

        public SpriteRenderSystem(EntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        public void Render(SpriteBatch spriteBatch)
        {
            List<Entity> _renderable = _entityManager.GetEntitiesWith( typeof(Sprite), typeof(Transform2D));

            _renderable = _renderable.OrderBy(entity => _entityManager.HasComponent<SpriteLayer>(entity) ? _entityManager.GetComponent<SpriteLayer>(entity).layer : -1)
            .ThenBy(entity => _entityManager.HasComponent<SpriteLayer>(entity) ? _entityManager.GetComponent<SpriteLayer>(entity).sortOrder : -1).ToList();

            foreach(Entity entity in _renderable){
                Sprite s = _entityManager.GetComponent<Sprite>(entity);
                Transform2D t = _entityManager.GetComponent<Transform2D>(entity);
                Vector2 pivot = _entityManager.HasComponent<Pivot>(entity) ? _entityManager.GetComponent<Pivot>(entity).PivotPoint : new Vector2(0, 0);
                spriteBatch.Draw(s.texture, t.position, null, Color.White, t.rotation, pivot, new Vector2(t.scale.X/ s.texture.Width, t.scale.Y / s.texture.Height), SpriteEffects.None, 0);
            }
        }
    }
}

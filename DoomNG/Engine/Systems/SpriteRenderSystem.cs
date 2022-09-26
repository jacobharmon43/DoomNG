using DoomNG.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace DoomNG.Engine.Systems
{
    /// <summary>
    /// Renders entities that contain the Sprite and Transform2D Components
    /// </summary>
    /// <seealso cref="DoomNG.Engine.ISystem" />
    internal class SpriteRenderSystem : ISystem
    {
        readonly EntityManager _entityManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteRenderSystem"/> class.
        /// </summary>
        /// <param name="entityManager"> The entity manager.</param>
        /// <param name="batch"> The batch.</param>
        public SpriteRenderSystem(EntityManager entityManager, SpriteBatch batch)
        {
            _entityManager = entityManager;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public void Execute() { }

        /// <summary>
        /// Renders sprites within the Sprite Batch
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Render(SpriteBatch spriteBatch)
        {
            List<Entity> _entitiesWithSprite = _entityManager.GetEntitiesWith<Sprite>();
            List<Entity> _entitiesWithTransform = _entityManager.GetEntitiesWith<Transform2D>();
            List<Entity> _renderable = _entitiesWithSprite.Intersect(_entitiesWithTransform).ToList();

            _renderable = _renderable.OrderBy(entity => _entityManager.HasComponent<SpriteLayer>(entity) ? _entityManager.GetComponent<SpriteLayer>(entity).layer : -1)
            .ThenBy(entity => _entityManager.HasComponent<SpriteLayer>(entity) ? _entityManager.GetComponent<SpriteLayer>(entity).sortOrder : -1).ToList();

            foreach(Entity entity in _renderable){
                Sprite s = _entityManager.GetComponent<Sprite>(entity);
                Transform2D t = _entityManager.GetComponent<Transform2D>(entity);
                Vector2 pivot = _entityManager.HasComponent<Pivot>(entity) ? _entityManager.GetComponent<Pivot>(entity).PivotPoint : new Vector2(0, 0);
                Rectangle desiredPos = new(t.position, t.scale);
                spriteBatch.Draw(s.texture, desiredPos, null, Color.White, t.rotation, new Vector2(pivot.X * t.scale.X, pivot.Y * t.scale.Y), SpriteEffects.None, 0);
            }
        }
    }
}

using DoomNG.Engine;
using DoomNG.Engine.Systems;
using DoomNG.Engine.Components;

using System.Collections.Generic;
using DoomNG.DoomSpire.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.VisualBasic;

namespace DoomNG.DoomSpire.Systems
{
    internal class PlayerSystem : ISystem
    {
        EntityManager _entityManager;
        LineRenderer _lineRenderer;
        GraphicsDevice _graphicsDevice;
        RaycastSystem _raycastSystem;

        Dictionary<Keys, Vector2> _points = new Dictionary<Keys, Vector2>()
        {
            {Keys.W, new Vector2(0,-0.05f) },
            {Keys.A, new Vector2(-0.05f,0) },
            {Keys.S, new Vector2(0,0.05f) },
            {Keys.D, new Vector2(0.05f,0) },
        };


        public PlayerSystem(EntityManager entityManager, LineRenderer lineRenderer, GraphicsDevice graphicsDevice, RaycastSystem raycastSystem)
        {
            _entityManager = entityManager;
            _lineRenderer = lineRenderer;
            _graphicsDevice = graphicsDevice;
            _raycastSystem = raycastSystem; 
        }

        public void Execute()
        {
            List<Entity> entities = _entityManager.GetEntitiesWith<Player>();
            foreach(Entity entity in entities)
            {
                Player p = _entityManager.GetComponent<Player>(entity);
                Transform2D t = _entityManager.GetComponent<Transform2D>(entity);

                Vector2 halfX = new Vector2(t.scale.X / 2, 0);
                Vector2 halfY = new Vector2(0, t.scale.Y / 2);
                Vector2 tinyY = new Vector2(0, t.scale.Y / 10);
                Vector2 tinyX = new Vector2(t.scale.X / 10, 0);
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    Vector2 origin1 = t.position - halfX + tinyX;
                    Vector2 origin2 = t.position + halfX - tinyX;
                    RaycastHit? r1 = _raycastSystem.LineCast(origin1, origin1 - halfY + _points[Keys.W]);
                    RaycastHit? r2 = _raycastSystem.LineCast(origin2, origin2 - halfY + _points[Keys.W]);
                    if (!r1.HasValue && !r2.HasValue)
                        t.Translate(_points[Keys.W] * 100);
                    _lineRenderer.AddLineToFrame(origin1, origin1 - halfY + _points[Keys.W]);
                    _lineRenderer.AddLineToFrame(origin2, origin2 - halfY + _points[Keys.W]);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    Vector2 origin1 = t.position - halfY + tinyY;
                    Vector2 origin2 = t.position + halfY - tinyY;
                    RaycastHit? r1 = _raycastSystem.LineCast(origin1, origin1 - halfX + _points[Keys.A]);
                    RaycastHit? r2 = _raycastSystem.LineCast(origin2, origin2 - halfX + _points[Keys.A]);
                    if (!r1.HasValue && !r2.HasValue)
                        t.Translate(_points[Keys.A] * 100);
                    _lineRenderer.AddLineToFrame(origin1, origin1 - halfX + _points[Keys.A]);
                    _lineRenderer.AddLineToFrame(origin2, origin2 - halfX + _points[Keys.A]);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    Vector2 origin1 = t.position - halfX + tinyX;
                    Vector2 origin2 = t.position + halfX - tinyX;
                    RaycastHit? r1 = _raycastSystem.LineCast(origin1, origin1 + halfY + _points[Keys.S]);
                    RaycastHit? r2 = _raycastSystem.LineCast(origin2, origin2 + halfY + _points[Keys.S]);
                    if (!r1.HasValue && !r2.HasValue)
                        t.Translate(_points[Keys.S] * 100);
                    _lineRenderer.AddLineToFrame(origin1, origin1 + halfY + _points[Keys.S]);
                    _lineRenderer.AddLineToFrame(origin2, origin2 + halfY + _points[Keys.S]);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    Vector2 origin1 = t.position - halfY + tinyY;
                    Vector2 origin2 = t.position + halfY - tinyY;
                    RaycastHit? r1 = _raycastSystem.LineCast(origin1, origin1 + halfX + _points[Keys.D]);
                    RaycastHit? r2 = _raycastSystem.LineCast(origin2, origin2 + halfX + _points[Keys.D]);
                    if (!r1.HasValue && !r2.HasValue)
                        t.Translate(_points[Keys.D] * 100);
                    _lineRenderer.AddLineToFrame(origin1, origin1 + halfX + _points[Keys.D]);
                    _lineRenderer.AddLineToFrame(origin2, origin2 + halfX + _points[Keys.D]);
                }
            }
        }
    }
}

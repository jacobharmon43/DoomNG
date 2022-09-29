using DoomNG.Engine;
using DoomNG.Engine.Systems;
using DoomNG.Engine.Components;

using System.Collections.Generic;
using DoomNG.DoomSpire.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace DoomNG.DoomSpire.Systems
{
    internal class PlayerSystem : ISystem
    {
        EntityManager _entityManager;
        LineRenderer _lineRenderer;
        GraphicsDevice _graphicsDevice;
        RaycastSystem _raycastSystem;

        const float MAXDELTA = 6;
        float mouseLastFrameX;

        Dictionary<Keys, Vector2> _points = new Dictionary<Keys, Vector2>()
        {
            {Keys.W, new Vector2(0,-1) },
            {Keys.A, new Vector2(-1,0) },
            {Keys.S, new Vector2(0,1) },
            {Keys.D, new Vector2(1,0) },
        };


        public PlayerSystem(EntityManager entityManager, LineRenderer lineRenderer, GraphicsDevice graphicsDevice, RaycastSystem raycastSystem)
        {
            _entityManager = entityManager;
            _lineRenderer = lineRenderer;
            _graphicsDevice = graphicsDevice;
            _raycastSystem = raycastSystem;
            mouseLastFrameX = Mouse.GetState().X;
        }

        public void Execute(GameTime gameTime)
        {
            MovePlayerCheckCollisions();
            GetVisibleVertices();

            List<Entity> playerEntities = _entityManager.GetEntitiesWith<Player>();
            Transform2D playerTransform = _entityManager.GetComponent<Transform2D>(playerEntities.FirstOrDefault());

            var mouseNow = Mouse.GetState();
            float mouseDiff = mouseNow.X - mouseLastFrameX;
            playerTransform.rotation += mouseDiff * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _lineRenderer.AddLineToFrame(playerTransform.position, playerTransform.position + playerTransform.forward * 64, Color.Black);
            mouseLastFrameX = mouseNow.X;
           
        }

        void GetVisibleVertices()
        {
            List<Entity> entities = _entityManager.GetEntitiesWith<BoxCollider>();
            List<Entity> playerEntities = _entityManager.GetEntitiesWith<Player>();
            Transform2D playerTransform = _entityManager.GetComponent<Transform2D>(playerEntities.FirstOrDefault());
            System.Diagnostics.Debug.WriteLine(entities.Count);
            foreach (Entity entity in entities)
            {
                BoxCollider bc = _entityManager.GetComponent<BoxCollider>(entity);
                Vector2[] vertices = bc.GetVertices();

                foreach (Vector2 vertice in vertices)
                {
                    RaycastHit? r = _raycastSystem.LineCast(playerTransform.position, vertice);
                    if (r.HasValue && r.Value.point == vertice)
                    {
                        _lineRenderer.AddLineToFrame(playerTransform.position, r.Value.point);
                    }
                }
            }
        }

        void MovePlayerCheckCollisions()
        {
            List<Entity> entities = _entityManager.GetEntitiesWith<Player>();
            foreach (Entity entity in entities)
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
                        t.Translate(_points[Keys.W] * 3);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    Vector2 origin1 = t.position - halfY + tinyY;
                    Vector2 origin2 = t.position + halfY - tinyY;
                    RaycastHit? r1 = _raycastSystem.LineCast(origin1, origin1 - halfX + _points[Keys.A]);
                    RaycastHit? r2 = _raycastSystem.LineCast(origin2, origin2 - halfX + _points[Keys.A]);
                    if (!r1.HasValue && !r2.HasValue)
                        t.Translate(_points[Keys.A] * 3);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    Vector2 origin1 = t.position - halfX + tinyX;
                    Vector2 origin2 = t.position + halfX - tinyX;
                    RaycastHit? r1 = _raycastSystem.LineCast(origin1, origin1 + halfY + _points[Keys.S]);
                    RaycastHit? r2 = _raycastSystem.LineCast(origin2, origin2 + halfY + _points[Keys.S]);
                    if (!r1.HasValue && !r2.HasValue)
                        t.Translate(_points[Keys.S] * 3);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    Vector2 origin1 = t.position - halfY + tinyY;
                    Vector2 origin2 = t.position + halfY - tinyY;
                    RaycastHit? r1 = _raycastSystem.LineCast(origin1, origin1 + halfX + _points[Keys.D]);
                    RaycastHit? r2 = _raycastSystem.LineCast(origin2, origin2 + halfX + _points[Keys.D]);
                    if (!r1.HasValue && !r2.HasValue)
                        t.Translate(_points[Keys.D] * 3);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    t.rotation += 0.05f;
                }
            }
        }
    }
}

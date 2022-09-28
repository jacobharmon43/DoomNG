using DoomNG.Engine;
using DoomNG.Engine.Systems;
using DoomNG.Engine.Components;

using System.Collections.Generic;
using DoomNG.DoomSpire.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace DoomNG.DoomSpire.Systems
{
    internal class PlayerSystem : ISystem
    {
        EntityManager _entityManager;
        LineRenderer _lineRenderer;
        GraphicsDevice _graphicsDevice;

        public PlayerSystem(EntityManager entityManager, LineRenderer lineRenderer, GraphicsDevice graphicsDevice)
        {
            _entityManager = entityManager;
            _lineRenderer = lineRenderer;
            _graphicsDevice = graphicsDevice;
        }

        public void Execute()
        {
            List<Entity> entities = _entityManager.GetEntitiesWith<Player>();
            foreach(Entity entity in entities)
            {
                Player p = _entityManager.GetComponent<Player>(entity);
                Transform2D t = _entityManager.GetComponent<Transform2D>(entity);

                switch (p.current)
                {
                    case Player.State.Idle:
                        Point mousePos = new Point(Mouse.GetState().X, Mouse.GetState().Y);
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed && _graphicsDevice.Viewport.Bounds.Contains(mousePos))
                        {
                            p.SetDesiredPoint(mousePos);
                            p.current = Player.State.Moving;
                        }
                        break;
                    case Player.State.Moving:
                        Vector2 v1 = new Vector2(p.desiredPoint.X, p.desiredPoint.Y);
                        Vector2 v2 = new Vector2(t.position.X, t.position.Y);
                        Vector2 diff = v1 - v2;
                        Vector2 movement = Vector2.Normalize(diff);
                        Point movement2 = new Point((int)(movement.X * 5), (int)(movement.Y * 5));
                        t.Translate(movement2);
                        _lineRenderer.AddLineToFrame(t.position, p.desiredPoint);
                        if (Vector2.Distance(v1,v2) <= 5)
                        {
                            p.current = Player.State.Idle;
                        }
                        break;
                }      
            }
        }
    }
}

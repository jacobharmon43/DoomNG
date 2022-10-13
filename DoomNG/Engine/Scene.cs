using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using DoomNG.Engine.Components;
using DoomNG.Engine.Systems;

namespace DoomNG.Engine
{
    internal abstract class Scene
    {
        List<GameObject> Objects;
        int LayerOrder = 0;
        public PhysicsSystem Physics;

        public GameObject[] GetObjects() => Objects.ToArray();

        public virtual void Initialize()
        {
            Objects = new List<GameObject>();
            Physics = new PhysicsSystem(this);
        }

        public void AddObject(GameObject obj)
        {
            obj.ownerScene = this;
            Objects.Add(obj);
            foreach(IComponent component in obj.GetComponents())
            {
                component.Awake();
            }
            foreach (IComponent component in obj.GetComponents())
            {
                component.Start();
            }
        }

        public virtual void Update()
        {
            foreach (GameObject obj in Objects)
            {
                foreach (IComponent component in obj.GetComponents())
                {
                    component.Update();
                }
            }
        }

        public virtual void LateUpdate()
        {
            foreach (GameObject obj in Objects)
            {
                foreach (IComponent component in obj.GetComponents())
                {
                    component.Update();
                }
            }
        }

        public virtual void PhysicsUpdate()
        {
            foreach (GameObject obj in Objects)
            {
                foreach (IComponent component in obj.GetComponents())
                {
                    component.LateUpdate();
                }
            }
        }

        public virtual void Render(SpriteBatch batch)
        {
            foreach (GameObject obj in Objects)
            {
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                Transform2D t = obj.GetComponent<Transform2D>();

                if (sr != null && t != null)
                {
                    batch.Draw(sr.Texture, t.position, null, sr.Color, t.rotation, t.pivot, new Vector2(t.scale.X / sr.Texture.Width, t.scale.Y / sr.Texture.Height), SpriteEffects.None, 0);
                }
            }
        }
    }
}

using DoomNG.Engine;
using DoomNG.Engine.Systems;
using DoomNG.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DoomNG.FroggyJump.Components
{
    internal class PlayerController : IComponent
    {
        Transform2D _transform;
        float _speed = 5;

        public PlayerController() { }
        public PlayerController(PlayerController other) { }

        public override void Awake()
        {
            _transform = gameObject.GetComponent<Transform2D>();
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            KeyboardQuery.UpdateKeyboard();
            int xDir = KeyboardQuery.CheckAxis(Keys.A, Keys.D);
            _transform.position.X += xDir * _speed;

            RaycastHit2D? ray = gameObject.OwnerScene.Physics.Linecast(_transform.position, _transform.position + Vector2.UnitX* 500);
            Gizmos.AddLineToRender(new Line(_transform.position, _transform.position + Vector2.UnitX * 500), ray.HasValue ? Color.Red : Color.White);
        }

        public override object Clone()
        {
            return new PlayerController(this);
        }
    }
}

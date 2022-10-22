using DoomNG.Engine;
using DoomNG.Engine.Types;
using DoomNG.Engine.Systems;
using DoomNG.Engine.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DoomNG.FroggyJump.Components
{
    internal class PlayerController : Component
    {
        Transform2D _transform;
        ContactFilter _filter;
        float _speed = 100;

        public PlayerController() { }
        public PlayerController(PlayerController other) { }

        public override void Awake()
        {
            _transform = gameObject.GetComponent<Transform2D>();
            _filter = new ContactFilter(new GameObject[1] {gameObject}, FilterType.BlackList);
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            KeyboardQuery.UpdateKeyboard();
            int xDir = KeyboardQuery.CheckAxis(Keys.A, Keys.D);
            _transform.position.X += xDir * _speed * Time.deltaTime;

            RaycastHit2D? ray = gameObject.OwnerScene.Physics.Linecast(_transform.position, _transform.position + Vector2.UnitX* 500, _filter);
            Gizmos.DrawLine(new Line(_transform.position, _transform.position + Vector2.UnitX * 500), ray.HasValue ? Color.Red : Color.White);
        }

        public override object Clone()
        {
            return new PlayerController(this);
        }
    }
}

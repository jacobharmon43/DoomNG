using DoomNG.Engine;
using DoomNG.Engine.Components;
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
        }

        public override object Clone()
        {
            return new PlayerController(this);
        }
    }
}

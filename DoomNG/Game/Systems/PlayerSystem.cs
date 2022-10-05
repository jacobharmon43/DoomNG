using System.Linq;
using System.Collections.Generic;

using DoomNG.Engine.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using DoomNG.Engine;
using DoomNG.Engine.Systems;
using DoomNG.Engine.Components;

using DoomNG.DoomSpire.Components;

using FSA;

namespace DoomNG.DoomSpire.Systems
{
    internal class PlayerSystem : ISystem
    {
        EntityManager _entityManager;
        RaycastSystem _raycastSystem;
        Texture2D _playerTexture;

        Entity _playerEntity;
        Transform2D _transform;

        float _movementSpeed = 3;

        const int collisionDivisions = 8;

        StateMachine _playerStateMachine;

        float _verticalVelocity = 0;
        float _deltaTime;

        Dictionary<Keys, Vector2> _points = new Dictionary<Keys, Vector2>()
        {
            {Keys.W, new Vector2(0,-1) },
            {Keys.A, new Vector2(-1,0) },
            {Keys.S, new Vector2(0,1) },
            {Keys.D, new Vector2(1,0) },
        };


        public PlayerSystem(EntityManager entityManager, RaycastSystem raycastSystem)
        {
            _entityManager = entityManager;
            _raycastSystem = raycastSystem;

            _playerTexture = TextureDistributor.GetTexture("Player");
        }

        Command MoveRight = new Command();
        Command MoveLeft = new Command();
        Command Jump = new Command();

        public void Execute(GameTime gameTime)
        {
            if (_transform == null) return;
            Camera c = _entityManager.GetComponents<Camera>().FirstOrDefault();
            c.Follow(_transform, new Vector3(0, 0, 0));
            _deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _playerStateMachine.RunStateMachine(_deltaTime);
            _transform.position += Vector2.UnitY * _verticalVelocity;

        }

        bool NDividedDirectionalCollisionDetection(int divisions, Vector2 direction, Transform2D transform)
        {
            bool inXDirection = direction.Y == 0;
            float sign = inXDirection ? (direction.X > 0 ? -1 : 1) : (direction.Y > 0 ? -1 : 1);

            Vector2 firstPosition = transform.position - (inXDirection ? Vector2.UnitY * transform.scale.Y : Vector2.UnitX * transform.scale.X) / 2;
            Vector2 pointIncrement = (inXDirection ? Vector2.UnitY * transform.scale.Y : Vector2.UnitX * transform.scale.X) / (divisions - 1);

            Vector2[] origins = new Vector2[divisions];
            for (int i = 0; i < divisions; i++)
            {
                origins[i] = firstPosition + pointIncrement * i;
            }

            float tinyIncrement = (inXDirection ? transform.scale.X : transform.scale.Y) / 20;
            float rayLength = (inXDirection ? transform.scale.X : transform.scale.Y) / 2 + tinyIncrement;
            bool returnValue = false;
            foreach (Vector2 point in origins)
            {
                RaycastHit? raycastHit = _raycastSystem.LineCast(point, point + direction * rayLength);
                returnValue |= raycastHit.HasValue;
            }
            return returnValue;
        }

        public void CreatePlayer()
        {
                

            _playerEntity = _entityManager.CreateEntity(new Sprite(_playerTexture), new Transform2D(Vector2.Zero, new Vector2(64, 64), 0), new Player(), new Pivot(0.5f, 0.5f), new SpriteLayer(1, 1));
            _transform = _entityManager.GetComponent<Transform2D>(_playerEntity);

            MoveLeft.Execute = () =>
            {
                if (!NDividedDirectionalCollisionDetection(collisionDivisions, _points[Keys.A], _transform))
                    _transform.Translate(_points[Keys.A] * _movementSpeed);
            };

            MoveRight.Execute = () =>
            {
                if (!NDividedDirectionalCollisionDetection(collisionDivisions, _points[Keys.D], _transform))
                    _transform.Translate(_points[Keys.D] * _movementSpeed);
            };

            Jump.Execute = () =>
            {
                if(_playerStateMachine.CurrentState.Name == "Grounded")
                    _verticalVelocity -= 5;
            };

            KeyboardQuery.AddCommand(Keys.A, KeyboardQuery.KeyPressState.Pressed, MoveLeft);
            KeyboardQuery.AddCommand(Keys.D, KeyboardQuery.KeyPressState.Pressed, MoveRight);
            KeyboardQuery.AddCommand(Keys.Space, KeyboardQuery.KeyPressState.Started, Jump);


            _playerStateMachine = new StateMachineBuilder()
                .WithState("Grounded")
                .WithOnEnter(() => _verticalVelocity = 0)
                .WithTransition("Falling", () => !NDividedDirectionalCollisionDetection(collisionDivisions, Vector2.UnitY, _transform))

                .WithState("Falling")
                .WithOnRun(() => _verticalVelocity += 10 * _deltaTime)
                .WithTransition("Grounded", () => NDividedDirectionalCollisionDetection(collisionDivisions, Vector2.UnitY, _transform))

                .Build();
        }

        public void RemovePlayer()
        {
            KeyboardQuery.RemoveCommand(Keys.A, KeyboardQuery.KeyPressState.Pressed, MoveLeft);
            KeyboardQuery.RemoveCommand(Keys.D, KeyboardQuery.KeyPressState.Pressed, MoveRight);
            KeyboardQuery.RemoveCommand(Keys.Space, KeyboardQuery.KeyPressState.Started, Jump);

            MoveLeft.Execute = null;
            MoveRight.Execute = null;
            Jump.Execute = null;

            _entityManager.RemoveEntity(_playerEntity);
            _transform = null;
        }
    }
}

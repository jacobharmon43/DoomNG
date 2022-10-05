using Microsoft.Xna.Framework;

namespace DoomNG.Engine.Components
{
    internal class Camera : IComponent
    {
        public Matrix Transform { get; private set; }
        Vector3 baseOffset;

        public Camera(Vector3 baseOffset)
        {
            this.baseOffset = baseOffset;
        }

        public void SetTransform(Matrix set) => Transform = set;

        public void Follow(Transform2D target, Vector3 additionalOffset)
        {
            var position = Matrix.CreateTranslation(-target.position.X - (target.scale.X / 2),
                                                    -target.position.Y - (target.scale.Y / 2),
                                                    0);
            var offsetTranslation = Matrix.CreateTranslation(baseOffset + additionalOffset);
            Transform = position * offsetTranslation;
        }
    }
}

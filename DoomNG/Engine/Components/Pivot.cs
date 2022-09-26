using Microsoft.Xna.Framework;

namespace DoomNG.Engine.Components
{
    /// <summary>
    /// Class for setting the pivot of a drawn object
    /// Defaults to (0,0)
    /// </summary>
    internal class Pivot : IComponent
    {
        public Vector2 PivotPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pivot"/> class.
        /// </summary>
        /// <param name="pivotPoint">The pivot point. (0,0) to (1,1) for most cases </param>
        public Pivot(Vector2 pivotPoint)
        {
            PivotPoint = pivotPoint;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pivot"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Pivot(float x, float y)
        {
            PivotPoint = new Vector2(x, y);
        }
    }
}

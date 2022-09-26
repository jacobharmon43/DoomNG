namespace DoomNG.Engine.Components
{
    /// <summary>
    /// Determines order of rendering
    /// Rendered Objects without this class are all set to values of -1, and rendered then in creation order
    /// </summary>
    /// <seealso cref="DoomNG.Engine.IComponent" />
    internal class SpriteLayer : IComponent
    {
        public int layer;
        public int sortOrder;

        public SpriteLayer(int layer, int sortOrder)
        {
            this.layer = layer;
            this.sortOrder = sortOrder;
        }
    }
}

namespace DoomNG.Engine.Helpers
{
    public enum Layer { Background, Terrain, Player, Foreground, UI };
    static internal class Layers
    {
        public static int GetLayer(Layer layer)
        {
            return (int)layer;
        }
    }
}

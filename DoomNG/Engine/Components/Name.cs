namespace DoomNG.Engine.Components
{
    /// <summary>
    /// Stores string information about an object
    /// </summary>
    /// <seealso cref="DoomNG.Engine.IComponent" />
    public class Name : IComponent
    {
        public string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="Name"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Name(string name) => this.name = name;
    }
}

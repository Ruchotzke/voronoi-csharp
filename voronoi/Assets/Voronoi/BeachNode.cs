namespace Voronoi
{
    /// <summary>
    /// An abstract implementation of a beach node.
    /// </summary>
    public abstract class BeachNode
    {
        /// <summary>
        /// True if this node is an arc, otherwise false.
        /// </summary>
        public bool IsArc;

        /// <summary>
        /// Compute the X position of this beach node.
        /// </summary>
        /// <param name="y">The y value of the beach line.</param>
        /// <returns>An x value, or throws an exception.</returns>
        public abstract float GetXPosition(float y);
    }
}
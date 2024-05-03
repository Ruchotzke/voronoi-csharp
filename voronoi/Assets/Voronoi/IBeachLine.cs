using UnityEngine;

namespace Voronoi
{
    /// <summary>
    /// An interface for a beach line.
    /// </summary>
    public interface IBeachLine<T> where T : BeachNode
    {
        /// <summary>
        /// Insert a new site into this beach line.
        /// </summary>
        /// <param name="site"></param>
        public void InsertSite(Site site);
        
        /// <summary>
        /// Delete a supplied arc from ths beach line.
        /// </summary>
        /// <param name="toDelete"></param>
        public void DeleteArc(T toDelete);

        /// <summary>
        /// Get the node directly above a given position.
        /// </summary>
        /// <param name="position">The position to query.</param>
        /// <returns>The node above, otherwise null if the beach line is empty.</returns>
        public T GetNodeAbove(Vector2 position);

        /// <summary>
        /// Return the leftmost node from the beachline.
        /// </summary>
        /// <returns></returns>
        public T GetLeftmostNode();
    }
}
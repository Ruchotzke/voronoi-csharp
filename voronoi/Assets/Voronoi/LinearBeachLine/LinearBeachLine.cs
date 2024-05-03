using System.Collections.Generic;
using UnityEngine;

namespace Voronoi.LinearBeachLine
{
    /// <summary>
    /// A linear implementation of a beach line.
    /// </summary>
    public class LinearBeachLine : IBeachLine<LinearBeachNode>
    {
        /// <summary>
        /// The list containing the beach nodes.
        /// </summary>
        public List<LinearBeachNode> Line;

        /// <summary>
        /// Construct a new beach line.
        /// </summary>
        public LinearBeachLine()
        {
            Line = new List<LinearBeachNode>();
        }
        
        public void InsertSite(Site site)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteArc(LinearBeachNode toDelete)
        {
            throw new System.NotImplementedException();
        }

        public LinearBeachNode GetNodeAbove(Vector2 position)
        {
            throw new System.NotImplementedException();
        }
    }
}
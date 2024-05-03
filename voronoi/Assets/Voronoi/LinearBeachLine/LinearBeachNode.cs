using System;
using UnityEngine;

namespace Voronoi.LinearBeachLine
{
    /// <summary>
    /// A beach node for a linear beach lne.
    /// </summary>
    public class LinearBeachNode : BeachNode
    {
        /// <summary>
        /// Generate an arc for a given site.
        /// </summary>
        /// <param name="site">The site this arc is formed from.</param>
        public LinearBeachNode(Site site)
        {
            ArcSite = site;
            IsArc = true;
        }

        /// <summary>
        /// Generate an intersection node.
        /// </summary>
        /// <param name="left">The arc to the left.</param>
        /// <param name="right">The arc to the right.</param>
        public LinearBeachNode(LinearBeachNode left, LinearBeachNode right)
        {
            LeftNode = left;
            RightNode = right;
            
            /* Also link other sides */
            left.RightNode = this;
            right.LeftNode = this;
        }
    }
}
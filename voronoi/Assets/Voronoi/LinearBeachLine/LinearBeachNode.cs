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
        /// The site this arc represents.
        /// </summary>
        public Site ArcSite;

        /// <summary>
        /// The node lying to the left of this one.
        /// </summary>
        public LinearBeachNode Left;

        /// <summary>
        /// The node lying to the right of this one.
        /// </summary>
        public LinearBeachNode Right;

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
            Left = left;
            Right = right;
            
            /* Also link other sides */
            left.Right = this;
            right.Left = this;
        }
        
        public override float GetXPosition(float y)
        {
            // DESMOS: https://www.desmos.com/calculator/fvmzdjhabt
            
            /* If this node is not an arc, throw an exception. */
            if (IsArc)
            {
                throw new ArithmeticException("Cannot compute the x position of an arc beach node.");
            }
            
            /* Grab both sites, and order them by y coordinate */
            Site upper = Left.ArcSite;
            Site lower = Right.ArcSite;
            if(upper.Position.y < lower.Position.y)
            {
                (upper, lower) = (lower, upper);
            }

            /* Make sure the beach line is below both points */
            if(lower.Position.y < y)
            {
                throw new ArgumentException("The beach line must be below any sites being used for x value computation.");
            }

            /* Degenerate case: if the points share a y value, the x intersection is just their midpoint */
            if(Math.Abs(upper.Position.y - lower.Position.y) < Constants.FLOAT_COMPARISON_TOLERANCE){
                return (upper.Position.x + lower.Position.x) / 2.0f;
            }
            
            /* Degenerate case: if the lower point is on the beach line, the x coordinate is just its x value */
            if (Math.Abs(lower.Position.y - y) < Constants.FLOAT_COMPARISON_TOLERANCE)
            {
                return lower.Position.x;
            }

            /* Now, compute parabola coefficients for each site (ax^2 + bx + c = 0) */
            float dyUp = upper.Position.y - y;
            float aUp = 1.0f / (2.0f * dyUp);
            float bUp = -upper.Position.x / dyUp;
            float cUp = (upper.Position.x * upper.Position.x + upper.Position.y * upper.Position.y - y * y) / (2.0f * dyUp);

            float dyDn = lower.Position.y - y;
            float aDn = 1.0f / (2.0f * dyDn);
            float bDn = -lower.Position.x / dyDn;
            float cDn = (lower.Position.x * lower.Position.x + lower.Position.y * lower.Position.y - y * y) / (2.0f * dyDn);

            /* Compute the correct intersection point */
            float da = aUp - aDn;
            float db = bUp - bDn;
            float dc = cUp - cDn;
            if(Left.ArcSite == upper){
                /* Use the + hlf of quadratic eq */
                return (-db + Mathf.Sqrt(db * db - 4 * da * dc)) / (2 * da);
            }
            else{
                /* Use the - hlf of quadratic eq */
                return (-db - Mathf.Sqrt(db * db - 4 * da * dc)) / (2 * da);
            }
        }
    }
}
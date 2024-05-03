using System;
using UnityEngine;

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
        /// The arc/intersection lying to the left of this one.
        /// </summary>
        public BeachNode LeftNode;

        /// <summary>
        /// The arc/intersection lying to the right of this one.
        /// </summary>
        public BeachNode RightNode;
        
        /// <summary>
        /// The site this arc represents.
        /// </summary>
        public Site ArcSite;

        /// <summary>
        /// Compute the X position of this beach node.
        /// </summary>
        /// <param name="y">The y value of the sweep line.</param>
        /// <returns>An x value, or throws an exception.</returns>
        public float GetXPosition(float y)
        {
            // DESMOS: https://www.desmos.com/calculator/fvmzdjhabt
            
            /* If this node is not an arc, throw an exception. */
            if (IsArc)
            {
                throw new ArithmeticException("Cannot compute the x position of an arc beach node.");
            }
            
            /* Grab both sites, and order them by y coordinate */
            Site upper = LeftNode.ArcSite;
            Site lower = RightNode.ArcSite;
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
            if(LeftNode.ArcSite == upper){
                /* Use the + hlf of quadratic eq */
                return (-db + Mathf.Sqrt(db * db - 4 * da * dc)) / (2 * da);
            }
            else{
                /* Use the - hlf of quadratic eq */
                return (-db - Mathf.Sqrt(db * db - 4 * da * dc)) / (2 * da);
            }
        }
        
        /// <summary>
        /// Return the parabola coefficients for this arc.
        /// </summary>
        /// <param name="beachY">The y position of the sweep line.</param>
        /// <returns>three quadratic coefficients.</returns>
        /// <exception cref="Exception"></exception>
        public (float a, float b, float c) GetParabolaCoefficients(float sweepY)
        {
            if (!IsArc)
            {
                throw new ArgumentException("Intersections cannot be analyzed like parabolas.");
            }
            
            float dy = ArcSite.Position.y - sweepY;
            float a = 1.0f / (2.0f * dy);
            float b = -ArcSite.Position.x / dy;
            float c = (ArcSite.Position.x * ArcSite.Position.x + ArcSite.Position.y * ArcSite.Position.y - sweepY * sweepY) / (2.0f * dy);

            return (a, b, c);
        }
    }
}
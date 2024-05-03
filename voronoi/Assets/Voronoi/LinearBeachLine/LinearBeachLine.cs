using System;
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
            /* if there are no nodes in the beach line, just add it and return */
            if (Line.Count == 0)
            {
                Line.Add(new LinearBeachNode(site));
                return;
            }
            
            /* Find the node which this new site is directly underneath */
            /* if there is only one node on the beach line, then this node must be underneath it */
            LinearBeachNode under = null;
            if (Line.Count == 1)
            {
                under = Line[0];
            }
            /* otherwise, find it */
            int currIndex = 1;
            while (currIndex < Line.Count)
            {
                /* Check this x value */
                float x = Line[currIndex].GetXPosition(site.Position.y);
                
                /* If identical, we're under an intersection. */
                if (Math.Abs(x - site.Position.x) < Constants.FLOAT_COMPARISON_TOLERANCE)
                {
                    under = Line[currIndex];
                    break;
                }
                
                /* If greater than the site's x position, we know the arc is to the left */
                if (x > site.Position.x)
                {
                    currIndex -= 1;
                    under = Line[currIndex];
                    break;
                }
                
                /* Otherwise, move to the next intersection */
                currIndex += 2;
            }
            
            /* DEGENERACY: If this site is under an intersection, handle a special case */
            if (!under.IsArc)
            {
                //TODO: Implement
                /* Handle as a circle event, and then add a new arc for the new site */
            }

            /* DEGENERACY: If this site is directly to the right of the arc site, handle a special case */
            if (Math.Abs(under.ArcSite.Position.y - site.Position.y) < Constants.FLOAT_COMPARISON_TOLERANCE)
            {
                if (under.ArcSite.Position.x < site.Position.x)
                {
                    /* Generate nodes */
                    LinearBeachNode newNode = new LinearBeachNode(site);
                    LinearBeachNode leftToNew = new LinearBeachNode(under, newNode);
                    
                    /* Link old neighbors, if applicable */
                    if (under.RightNode != null) under.RightNode.LeftNode = leftToNew;
                    
                    /* Insert the new sites */
                    Line.Insert(currIndex + 1, newNode);
                    Line.Insert(currIndex + 1, leftToNew);
                }
                else
                {
                    /* Generate nodes */
                    LinearBeachNode newNode = new LinearBeachNode(site);
                    LinearBeachNode newToRight = new LinearBeachNode(newNode, under);
                    
                    /* Link old neighbors, if applicable */
                    if (under.LeftNode != null) under.LeftNode.RightNode = newToRight;
                    
                    /* Insert the new sites */
                    Line.Insert(currIndex, newToRight);
                    Line.Insert(currIndex, newNode);
                }

                return;
            }
            
            /* If we've made it here, split the arc into three pieces: [A, AB, B, BA, A] */
            LinearBeachNode left = new LinearBeachNode(under.ArcSite);
            LinearBeachNode center = new LinearBeachNode(site);
            LinearBeachNode right = new LinearBeachNode(under.ArcSite);
            LinearBeachNode leftToCenter = new LinearBeachNode(left, center);
            LinearBeachNode centerToRight = new LinearBeachNode(center, right);
            
            /* Update old node's left/right neighbors */
            if(under.LeftNode != null) under.LeftNode.RightNode = left;
            if(under.RightNode != null) under.RightNode.LeftNode = right;
            
            /* Remove the old node */
            Line.Remove(under);
            
            /* Insert new nodes (reverse order to maintain correct order */
            Line.Insert(currIndex, right);
            Line.Insert(currIndex, centerToRight);
            Line.Insert(currIndex, center);
            Line.Insert(currIndex, leftToCenter);
            Line.Insert(currIndex, left);
        }

        public void DeleteArc(LinearBeachNode toDelete)
        {
            throw new System.NotImplementedException();
        }

        public LinearBeachNode GetNodeAbove(Vector2 position)
        {
            throw new System.NotImplementedException();
        }

        public LinearBeachNode GetLeftmostNode()
        {
            if (Line.Count == 0) return null;
            return Line[0];
        }
    }
}
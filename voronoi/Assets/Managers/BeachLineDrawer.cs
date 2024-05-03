using UnityEngine;
using Voronoi;

namespace Managers
{
    public class BeachLineDrawer : MonoBehaviour
    {
        [Header("Parameters")] 
        public int SegmentCount = 100;
        public float MinX = -100;
        public float MaxX = 100;

        [Header("Prefabs")] public LineRenderer pf_Arc;

        /// <summary>
        /// Draw a beach line from the supplied beach line.
        /// </summary>
        public void DrawBeachLine(IBeachLine<BeachNode> beachLine, float sweepY, float lineWidth, Color lineColor)
        {
            /* First clear out any existing lines */
            ClearBeachLine();
            
            /* Now draw any arcs */
            BeachNode curr = beachLine.GetLeftmostNode();
            while (curr != null)
            {
                GenerateArc(curr, sweepY, lineWidth, lineColor);
                curr = curr.RightNode?.RightNode;
            }
        }

        /// <summary>
        /// Clear out all current entities from the beach line.
        /// </summary>
        public void ClearBeachLine()
        {
            foreach (var child in transform.GetComponentsInChildren<LineRenderer>())
            {
                Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Generate an arc for a given arc.
        /// </summary>
        /// <param name="arc"></param>
        private void GenerateArc(BeachNode arc, float sweepY, float lineWidth, Color lineColor)
        {
            /* Create a new instance */
            var instance = Instantiate(pf_Arc, transform);
            
            /* Compute the shape of the arc */
            float left = MinX;
            float right = MaxX;
            if (arc.LeftNode != null)
            {
                left = arc.LeftNode.GetXPosition(sweepY);
            }
            if (arc.RightNode != null)
            {
                right = arc.RightNode.GetXPosition(sweepY);
            }

            var coeff = arc.GetParabolaCoefficients(sweepY);
            
            /* Update the line renderer look */
            instance.startWidth = lineWidth;
            instance.endWidth = lineWidth;
            instance.startColor = lineColor;
            instance.endColor = lineColor;
            
            /* Sample the parabola and generate points */
            Vector3[] line = new Vector3[SegmentCount];
            line[0] = new Vector2(left, coeff.a * left * left + coeff.b * left + coeff.c);
            line[SegmentCount-1] = new Vector2(right, coeff.a * right * right + coeff.b * right + coeff.c);
            float dist = (right - left) / (SegmentCount - 1);
            for (int i = 1; i < SegmentCount - 1; i++)
            {
                float x = dist * i;
                line[i] = new Vector2(x, coeff.a * x * x + coeff.b * x + coeff.c);
            }
            
            /* Update the line renderer */
            instance.SetPositions(line);
        }
    
    }
}

using UnityEngine;

namespace Managers
{
    public class SweepLineDrawer : MonoBehaviour
    {
        private LineRenderer _lr;

        public float MinX = -100;
        public float MaxX = 100;
    
        private void Awake()
        {
            _lr = GetComponent<LineRenderer>();
        }

        /// <summary>
        /// Move the sweep line to the appropriate position on the screen.
        /// </summary>
        /// <param name="yPos">The y value of the sweep line.</param>
        /// <param name="width">The display width.</param>
        /// <param name="color">The color.</param>
        public void DrawSweepLine(float yPos, float width, Color color)
        {
            _lr.startColor = color;
            _lr.endColor = color;
            _lr.startWidth = width;
            _lr.endWidth = width;
            _lr.SetPosition(0, new Vector3(MinX, yPos));
            _lr.SetPosition(1, new Vector3(MaxX, yPos));
        }
    
    
    }
}

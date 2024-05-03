using UnityEngine;
using Voronoi.LinearBeachLine;

namespace Managers
{
    public class DisplayManager : MonoBehaviour
    {
        [Header("Parameters")] 
        public float SweepY = 10;       /* The Y coordinate of the sweep line */

        [Header("Sweep Line")] 
        public float SweepLineWidth = 0.03f;
        public Color SweepLineColor = Color.yellow;
        public float BeachLineWidth = 0.03f;
        public Color BeachLineColor = Color.green;
    
        [Header("Drawing Components")] 
        public SweepLineDrawer SweepLineDrawer;

        public BeachLineDrawer BeachLineDrawer;
    
        /* Instance Variable */
        public static DisplayManager Instance;

        /* Private Components */
        private LinearBeachLine _beachLine;
        
        private void Awake()
        {
            /* Handle singleton */
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            _beachLine = new LinearBeachLine();
        }

        private void LateUpdate()
        {
            /* Draw the sweep line */
            SweepLineDrawer.DrawSweepLine(SweepY, SweepLineWidth, SweepLineColor);
        }
    }
}

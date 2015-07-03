using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    class LateralRaiseDownPattern : IActionPattern
    {
        //Hardcoded values of the pattern, for position and constraint
        private const double Y_POSITION = 0.40;
        private const double Z_POSITION = 0.70;
        /// <summary>
        /// fired when pattern is recognised
        /// </summary>
        public event ActionDelegate onActionDone;

        /// <summary>
        /// Process the accelometer data to see if lateral raise down pattern is recognised
        /// if recognised fire onActionDone Event
        /// check also if pattern is correctly done
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void processAccelData(double x, double y, double z)
        {
            
            if (z > Z_POSITION && y > Y_POSITION )
            {
                if (x > 0.1)
                {
                    Debug.Print("Too much rotation out!");
                    onActionDone(Quality.BAD, "Too much rotation out!");
                } else if(x < -0.1) {
                    Debug.Print("Too much rotation in!");
                    onActionDone(Quality.BAD, "Too much rotation in!");
                }
                else
                {
                    Debug.Print("Down!!!");
                    onActionDone(Quality.GOOD, "");
                }
            }           
        }
    }
}

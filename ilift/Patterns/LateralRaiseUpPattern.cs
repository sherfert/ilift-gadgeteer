using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    class LateralRaiseUpPattern:IActionPattern
    {
        //Hardcoded values of the pattern, for position and constraint
        private const double Y_THRESHOLD = 0.08;
        private const double Z_POSITION = 0.9;

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
            // && x < RIGHT_BOUND && x > LEFT_BOUND && x < RIGHT_BOUND
            if (z > Z_POSITION && y < Y_THRESHOLD)
            {
                if (x > 0.25)
                {
                    Debug.Print("Too much rotation out!");
                    onActionDone(Quality.BAD, "Too much rotation out!");
                }
                else if (x < -0.2)
                {
                    Debug.Print("Too much rotation in!");
                    onActionDone(Quality.BAD, "Too much rotation in!");
                }
                else
                {
                    Debug.Print("Up!!!");
                    onActionDone(Quality.GOOD, "");
                }
            }          
        }
    }
}

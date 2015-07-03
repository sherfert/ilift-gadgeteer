using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    public class TricepExtensionDownPattern : IActionPattern
    {
        //Hardcoded values of the pattern, for position and constraint
        private const double X_POSITION = 0.1;
        private const double Y_POSITION = -0.05;
        private const double Z_POSITION = 0.9;

        /// <summary>
        /// fired when pattern is recognised
        /// </summary>
        public event ActionDelegate onActionDone;
        public TricepExtensionDownPattern()
        {
        }

        /// <summary>
        /// Process the accelometer data to see if tricep extension down pattern is recognised
        /// if recognised fire onActionDone Event
        /// check also if pattern is correctly done
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void processAccelData(double x, double y, double z)
        {
            if (z > Z_POSITION && y > Y_POSITION && x < X_POSITION )
            {
                    Debug.Print("Down!!!");
                    onActionDone(Quality.GOOD, "");

            }

        }
    }
}

using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    public class TricepExtensionUpPattern : IActionPattern
    {
        //Hardcoded values of the pattern, for position and constraint
       
        private const double X_POSITION = -0.4;
        private const double Y_POSITION = -0.3;
        private const double Z_POSITION = 1.2;

        /// <summary>
        /// fired when pattern is recognised
        /// </summary>
        public event ActionDelegate onActionDone;

        public TricepExtensionUpPattern()
        {
        }

        /// <summary>
        /// Process the accelometer data to see if tricep extension up pattern is recognised
        /// if recognised fire onActionDone Event
        /// check also if pattern is correctly done
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void processAccelData(double x, double y, double z)
        {
            if (z > Z_POSITION && y < Y_POSITION  && x < X_POSITION/*&& x < RIGHT_BOUND && x > LEFT_BOUND*/)
            {
               
                Debug.Print("Up!!!");
                onActionDone(Quality.GOOD, "");
            

            }

        }
    }
}

using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    public class CurlDownPattern : IActionPattern
    {
        //Hardcoded values of the pattern, for position and constraint
        private const double LEFT_BOUND = 0.20;
        private const double RIGHT_BOUND = -0.20;
        private const double Y_POSITION = 0.55;
        private const double Z_POSITION = 0.5;

        /// <summary>
        /// fired when pattern is recognised
        /// </summary>
        public event ActionDelegate onActionDone;
        public CurlDownPattern()
        {
        }

        /// <summary>
        /// Process the accelometer data to see if curl down pattern is recognised
        /// if recognised fire onActionDone Event
        /// check also if pattern is correctly done
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void processAccelData(double x, double y, double z)
        {
            if (z > Z_POSITION && y > Y_POSITION)
            {

                if (x < RIGHT_BOUND)
                {
                    Debug.Print("Too much left!");
                    onActionDone(Quality.BAD, "Too much left");

                }
                else if (x > LEFT_BOUND)
                {
                    Debug.Print("Too much right!");
                    onActionDone(Quality.BAD, "Too much right");
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

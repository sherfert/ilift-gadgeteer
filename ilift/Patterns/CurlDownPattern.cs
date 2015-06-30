using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    public class CurlDownPattern : IActionPattern
    {
        private const double LEFT_BOUND = 0.20;
        private const double RIGHT_BOUND = -0.20;
        private const double Y_POSITION = 0.55;
        private const double Z_POSITION = 0.5;

        public event ActionDelegate onActionDone;
        public CurlDownPattern()
        {
        }

        // TODO make more accurate specially with the wrong movement detection
        public void processAccelData(double x, double y, double z)
        {
            if (z > Z_POSITION && y > Y_POSITION)
            {
               
                //Debug.Print("Accelerometer:\tx: " + x + "\ty: " + y + "\tz: " + z + "\n");


                if (x < RIGHT_BOUND)
                {
                    Debug.Print("Too much right!");
                    onActionDone(Quality.BAD, "Too much right");

                }
                else if (x > LEFT_BOUND)
                {
                    Debug.Print("Too much left!");
                    onActionDone(Quality.BAD, "Too much left");
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

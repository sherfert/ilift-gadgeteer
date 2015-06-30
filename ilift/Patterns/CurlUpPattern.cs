using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    public class CurlUpPattern : IActionPattern
    {
        private const double LEFT_BOUND = 0.20;
        private const double RIGHT_BOUND = -0.15;
        private const double Y_POSITION = 0.2;
        private const double Z_POSITION = 1;
        public event ActionDelegate onActionDone;
        public CurlUpPattern()
        {
        }

        // TODO make more accurate specially with the wrong movement detection
        public void processAccelData(double x, double y, double z)
        {
            if (z > Z_POSITION && y < Y_POSITION)
            {
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
                    Debug.Print("Up!!!");
                    //Debug.Print("Accelerometer:\tx: " + x + "\ty: " + y + "\tz: " + z + "\n");
                    onActionDone(Quality.GOOD, "");
                }


            }


        }


    }
}

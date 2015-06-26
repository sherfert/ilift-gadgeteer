using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    class LateralRaiseDownPattern : IActionPattern
    {
        private const double LEFT_BOUND = -0.1;
        private const double RIGHT_BOUND = -0.2;
        private const double Y_POSITION = 0.1;
        private const double Z_POSITION = 0.85;

        public event ActionDelegate onActionDone;

        public void processAccelData(double x, double y, double z)
        {
            if (z > Z_POSITION && y > Y_POSITION && x < RIGHT_BOUND && x > LEFT_BOUND)
            {
                Debug.Print("Down!!!");
                //Debug.Print("Accelerometer:\tx: " + x + "\ty: " + y + "\tz: " + z + "\n");
                onActionDone();

            }
            else if (z > Z_POSITION && y > Z_POSITION && x > LEFT_BOUND && x < RIGHT_BOUND)
            {
                Debug.Print("Too much right!");
                onActionDone();

            }
            else if (z > Z_POSITION && y > Y_POSITION && x > LEFT_BOUND && x < RIGHT_BOUND)
            {
                Debug.Print("Too much left!");
                onActionDone();
            }
            
        }
    }
}

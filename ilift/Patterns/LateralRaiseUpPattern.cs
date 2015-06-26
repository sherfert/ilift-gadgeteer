using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    class LateralRaiseUpPattern:IActionPattern
    {
        private const double LEFT_BOUND = 0.0;
        private const double RIGHT_BOUND = 0.2;
        private const double Y_THRESHOLD = 0.1;
        private const double Z_POSITION = 1.15;

        public event ActionDelegate onActionDone;
        
        public void processAccelData(double x, double y, double z)
        {
            // && x < RIGHT_BOUND && x > LEFT_BOUND && x < RIGHT_BOUND
            if (z < Z_POSITION && y < Y_THRESHOLD)
            {
                Debug.Print("Up!!!");
                //Debug.Print("Accelerometer:\tx: " + x + "\ty: " + y + "\tz: " + z + "\n");
                onActionDone();

            }
            else if (z > Z_POSITION &&  y < Y_THRESHOLD && x > LEFT_BOUND && x < RIGHT_BOUND)
            {
                Debug.Print("Too much right!");
                onActionDone();

            }
            else if (z > Z_POSITION && y < Y_THRESHOLD && x > LEFT_BOUND && x < RIGHT_BOUND)
            {
                Debug.Print("Too much left!");
                onActionDone();

            }
          
        }
    }
}

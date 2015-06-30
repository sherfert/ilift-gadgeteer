using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    class LateralRaiseUpPattern:IActionPattern
    {
        private const double LEFT_BOUND = 0.0;
        private const double RIGHT_BOUND = 0.2;
        private const double Y_THRESHOLD = 0.08;
        private const double Z_POSITION = 0.9;

        public event ActionDelegate onActionDone;
        
        public void processAccelData(double x, double y, double z)
        {
            // && x < RIGHT_BOUND && x > LEFT_BOUND && x < RIGHT_BOUND
            if (z > Z_POSITION && y < Y_THRESHOLD)
            {
                if (x > 0.25)
                {
                    Debug.Print("Too much rotation up!");
                    onActionDone(Quality.BAD, "Too much rotation up!");
                }
                else if (x < -0.2)
                {
                    Debug.Print("Too much rotation down!");
                    onActionDone(Quality.BAD, "Too much rotation down!");
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

using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    class LateralRaiseDownPattern : IActionPattern
    {
        private const double LEFT_BOUND = -0.1;
        private const double RIGHT_BOUND = -0.2;
        private const double Y_POSITION = 0.40;
        private const double Z_POSITION = 0.70;

        public event ActionDelegate onActionDone;

        public void processAccelData(double x, double y, double z)
        {
            //&& x < RIGHT_BOUND && x > LEFT_BOUND
            if (z > Z_POSITION && y > Y_POSITION )
            {
                if (x > 0.1)
                {
                    Debug.Print("Too much rotation up!");
                    onActionDone(Quality.BAD, "Too much rotation up!");
                } else if(x < -0.1) {
                    Debug.Print("Too much rotation down!");
                    onActionDone(Quality.BAD, "Too much rotation down!");
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

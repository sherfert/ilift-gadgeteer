using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    class LateralRaiseDownPattern : IActionPattern
    {
        public event ActionDelegate onActionDone;

        public void processAccelData(double x, double y, double z)
        {
            if (z > 1.3 && y > 0.35)
            {
                Debug.Print("Down!!!");
                //Debug.Print("Accelerometer:\tx: " + x + "\ty: " + y + "\tz: " + z + "\n");
                onActionDone();

            }
            else
            {
                //Debug.Print("Accelerometer:\tx: " + x + "\ty: " + y + "\tz: " + z + "\n");
            }
            
        }
    }
}

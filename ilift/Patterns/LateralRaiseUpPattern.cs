using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    class LateralRaiseUpPattern:IActionPattern
    {
        public event ActionDelegate onActionDone;

        public void processAccelData(double x, double y, double z)
        {
            if (z < 1.1 && y < 0)
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

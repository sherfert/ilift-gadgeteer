using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    public class CurlDownPattern : IActionPattern
    {
        public event ActionDelegate onActionDone;
        public CurlDownPattern()
        {
        }

        // TODO make more accurate specially with the wrong movement detection
        public void processAccelData(double x, double y, double z)
        {
            if (z > 1.2 && y > 0.55 && x < 0.08 && x > -0.08)
            {
                Debug.Print("Down!!!");
                //Debug.Print("Accelerometer:\tx: " + x + "\ty: " + y + "\tz: " + z + "\n");
                onActionDone();
                
            }
            else if (z > 1.2 && y > 0.55 && x > 0.08)
            {
                Debug.Print("Too much right!");
                onActionDone();
               
            }
            else if (z > 1.2 && y > 0.55 && x < -0.08)
            {
                Debug.Print("Too much left!");
                onActionDone();
            }

            else
            {
                //Debug.Print("Accelerometer:\tx: " + x + "\ty: " + y + "\tz: " + z + "\n");
            }
        }

        
    }
}

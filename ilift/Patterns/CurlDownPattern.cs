using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    public class CurlDownPattern : IActionPattern
    {

        public CurlDownPattern()
        {
        }
    
        public void processAccelData(double x, double y, double z)
        {
            if (z > 1.2 && y > 0.6)
            {
                Debug.Print("Down!!!");
                onActionDone();
                
            }
            else
            {
                //Debug.Print("Accelerometer:\tx: " + x + "\ty: " + y + "\tz: " + z + "\n");
            }
        }

        public event ActionDelegate onActionDone;
    }
}

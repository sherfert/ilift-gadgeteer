using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    public class CurlUpPattern : IActionPattern
    {

        public CurlUpPattern()
        {
        }
    
        public void processAccelData(double x, double y, double z)
        {
            if (z < 1.1 && y < 0.2)
            {
                Debug.Print("Up!!!");
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

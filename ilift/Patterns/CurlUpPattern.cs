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
            if (z < 1.1 && y < 0.2 && x < 0.08 && x > -0.08)
            {
                Debug.Print("Up!!!");
                Debug.Print("Accelerometer:\tx: " + x + "\ty: " + y + "\tz: " + z + "\n");
                onActionDone();
                
            }
            else if(z < 1.1 && y < 0.2 && x > 0.08) {
                Debug.Print("Too much right!");
                onActionDone();
               
            }
            else if (z < 1.1 && y < 0.2 && x < -0.08)
            {
                Debug.Print("Too much left!");
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

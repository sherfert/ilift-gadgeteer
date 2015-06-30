using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    public class TricepExtensionDownPattern : IActionPattern
    {
        
        private const double FRONT_BOUND = 0.30;
        private const double X_POSITION = 0.1;
        private const double Y_POSITION = -0.05;
        private const double Z_POSITION = 0.9;

        public event ActionDelegate onActionDone;
        public TricepExtensionDownPattern()
        {
        }

        // TODO make more accurate specially with the wrong movement detection
        public void processAccelData(double x, double y, double z)
        {
            if (z > Z_POSITION && y > Y_POSITION && x < X_POSITION /*&& x < RIGHT_BOUND && x > LEFT_BOUND*/)
            {
              
                    Debug.Print("Down!!!");
                    //Debug.Print("Accelerometer:\tx: " + x + "\ty: " + y + "\tz: " + z + "\n");
                    onActionDone(Quality.GOOD, "");
                



            }

        }
    }
}

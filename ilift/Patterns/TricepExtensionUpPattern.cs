using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    public class TricepExtensionUpPattern : IActionPattern
    {
        private const double LEFT_BOUND = -0.08;
        private const double RIGHT_BOUND = 0.08;
        private const double Y_POSITION = -0.3;
        private const double Z_POSITION = 0.5;

        public event ActionDelegate onActionDone;
        public TricepExtensionUpPattern()
        {
        }

        // TODO make more accurate specially with the wrong movement detection
        public void processAccelData(double x, double y, double z)
        {
            if (z > Z_POSITION && y < Y_POSITION /*&& x < RIGHT_BOUND && x > LEFT_BOUND*/)
            {
                Debug.Print("Up!!!");
                //Debug.Print("Accelerometer:\tx: " + x + "\ty: " + y + "\tz: " + z + "\n");
                onActionDone();

            }
            else if (z > Z_POSITION && y < Y_POSITION && x > RIGHT_BOUND)
            {
                Debug.Print("Too much right!");
                onActionDone();

            }
            else if (z > Z_POSITION && y < Y_POSITION && x < LEFT_BOUND)
            {
                Debug.Print("Too much left!");
                onActionDone();
            }
        }
    }
}
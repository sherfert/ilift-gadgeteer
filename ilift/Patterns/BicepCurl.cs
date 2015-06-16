using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    class BicepCurl
    {
        private IActionPattern currentPattern;
        private CurlUpPattern cup;
        private CurlDownPattern cdown;
        private int repetition;

        public BicepCurl()
        {
            cdown = new CurlDownPattern();
            cup = new CurlUpPattern();
            cdown.onActionDone += cdown_onActionDone;
            cup.onActionDone += cup_onActionDone;
            repetition = 0;
            currentPattern = cdown;
        }

        void cup_onActionDone()
        {
            repetition++;
            currentPattern = cdown;
            Debug.Print("" + repetition);
        }

        void cdown_onActionDone()
        {
            currentPattern = cup;
        }

        public void processAccelData(double x, double y, double z)
        {
            currentPattern.processAccelData(x, y, z);
        }
    }
}

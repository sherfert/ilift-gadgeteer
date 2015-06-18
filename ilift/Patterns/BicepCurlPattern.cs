using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
   
    public class BicepCurlPattern:IActionPattern
    {
       
        public event ActionDelegate onActionDone;

        private IActionPattern currentPattern;
        private CurlUpPattern cup;
        private CurlDownPattern cdown;
        private int repetition;

        public BicepCurlPattern()
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
            onActionDone();
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

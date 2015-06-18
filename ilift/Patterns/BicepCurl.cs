using System;
using Microsoft.SPOT;
using System.Collections;

namespace ilift.Patterns
{
    //Wrapper class for BicepCurl Complex Pattern, perhaps could be considered an Exercise with an id?
    public class BicepCurl
    {

        public event ActionDelegate onRepetitionDone;
        private ComplexPattern bicepCurlPattern;

        public BicepCurl()
        {
            bicepCurlPattern = new ComplexPattern();
            bicepCurlPattern.addPattern(new CurlDownPattern());
            bicepCurlPattern.addPattern(new CurlUpPattern());
            bicepCurlPattern.onActionDone += repetition;
      
        }



        public void processData(double x, double y, double z)
        {
            bicepCurlPattern.processAccelData(x, y, z);
        }

        private void repetition()
        {
            onRepetitionDone();
        }


    }
}

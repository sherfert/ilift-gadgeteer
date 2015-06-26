using System;
using Microsoft.SPOT;
using System.Collections;
using Gadgeteer.Modules.GHIElectronics;

namespace ilift.Patterns
{
    //Wrapper class for BicepCurl Complex Pattern, perhaps could be considered an Exercise with an id?
    public class BicepCurl : AbstractExercise
    {

        //public event RepetitionDelegate onRepetitionDone;
        private ComplexPattern bicepCurlPattern;

        public BicepCurl()
        {
            bicepCurlPattern = new ComplexPattern();
            bicepCurlPattern.addPattern(new CurlDownPattern());
            bicepCurlPattern.addPattern(new CurlUpPattern());
            bicepCurlPattern.onActionDone += onRepetitionDoneHandler;
      
        }

        public override void ProcessData(double x, double y, double z)
        {
            bicepCurlPattern.processAccelData(x, y, z);
        }
    }
}

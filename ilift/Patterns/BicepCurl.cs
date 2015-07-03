using System;
using Microsoft.SPOT;
using System.Collections;
using Gadgeteer.Modules.GHIElectronics;

namespace ilift.Patterns
{
    /// <summary>
    /// The Higher level bicep curl pattern class
    /// </summary>
    public class BicepCurl : AbstractExercise
    {
        /// <summary>
        /// Contains subpatterns of bicep curl
        /// </summary>
        private ComplexPattern bicepCurlPattern;

        /// <summary>
        /// Initialize all the subpatterns
        /// </summary>
        public BicepCurl()
        {
            bicepCurlPattern = new ComplexPattern();
            bicepCurlPattern.addPattern(new CurlDownPattern());
            bicepCurlPattern.addPattern(new CurlUpPattern());
            bicepCurlPattern.onActionDone += onRepetitionDoneHandler;
            bicepCurlPattern.onSubPatternDone += onSubPatternDoneHandler;
        }

        /// <summary>
        /// The function that determines if the action is valid by passing 
        /// the data from accelerometer to subpattern 
        /// </summary>
        /// <param name="x"> The x coordinator of movement
        /// <param name="y"> The y coordinator of movement
        /// <param name="z"> The z coordinator of movement
        public override void ProcessData(double x, double y, double z)
        {
            bicepCurlPattern.processAccelData(x, y, z);
        }
    }
}

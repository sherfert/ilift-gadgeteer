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
        /// Initialize all the subpatterns
        /// </summary>
        public BicepCurl()
        {
            pattern = new ComplexPattern();
            pattern.addPattern(new CurlDownPattern());
            pattern.addPattern(new CurlUpPattern());
            pattern.onActionDone += onRepetitionDoneHandler;
            pattern.onSubPatternDone += onSubPatternDoneHandler;
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
            pattern.processAccelData(x, y, z);
        }
    }
}

using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;

namespace ilift.Patterns
{
    /// <summary>
    /// The Higher level tricep extension pattern class
    /// </summary>
    public class TricepExtension:AbstractExercise
    {
        /// <summary>
        /// Contains subpatterns of tricep extension
        /// </summary>
        private ComplexPattern tricepExtensionPattern;

        /// <summary>
        /// Initialize all the subpatterns
        /// </summary>
        public TricepExtension()
        {
            tricepExtensionPattern = new ComplexPattern();
            tricepExtensionPattern.addPattern(new TricepExtensionUpPattern());
            tricepExtensionPattern.addPattern(new TricepExtensionDownPattern());
            tricepExtensionPattern.onActionDone += onRepetitionDoneHandler;
            tricepExtensionPattern.onSubPatternDone += onSubPatternDoneHandler;

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
            tricepExtensionPattern.processAccelData(x, y, z);
        }
    }
}

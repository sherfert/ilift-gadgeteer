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
        /// Initialize all the subpatterns
        /// </summary>
        public TricepExtension()
        {
            pattern = new ComplexPattern();
            pattern.addPattern(new TricepExtensionUpPattern());
            pattern.addPattern(new TricepExtensionDownPattern());
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

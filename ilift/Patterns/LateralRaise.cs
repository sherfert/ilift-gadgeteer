using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;

namespace ilift.Patterns
{
    /// <summary>
    /// The Higher level lateral raise pattern class
    /// </summary>
    public class LateralRaise : AbstractExercise
    {
        /// <summary>
        /// Contains subpatterns of lateral raise
        /// </summary>
        private ComplexPattern lateralRaisePattern;

        /// <summary>
        /// Initialize all the subpatterns
        /// </summary>
        public LateralRaise()
        {
            lateralRaisePattern = new ComplexPattern();
            lateralRaisePattern.addPattern(new LateralRaiseDownPattern());
            lateralRaisePattern.addPattern(new LateralRaiseUpPattern());
            lateralRaisePattern.onActionDone += onRepetitionDoneHandler;
            lateralRaisePattern.onSubPatternDone += onSubPatternDoneHandler;

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
            lateralRaisePattern.processAccelData(x, y, z);
        }
    }
}

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
        /// Initialize all the subpatterns
        /// </summary>
        public LateralRaise()
        {
            pattern = new ComplexPattern();
            pattern.addPattern(new LateralRaiseDownPattern());
            pattern.addPattern(new LateralRaiseUpPattern());
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

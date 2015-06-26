using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    public class LateralRaise : AbstractExercise
    {
        
        private ComplexPattern lateralRaisePattern;

        public LateralRaise()
        {
            lateralRaisePattern = new ComplexPattern();
            lateralRaisePattern.addPattern(new LateralRaiseDownPattern());
            lateralRaisePattern.addPattern(new LateralRaiseUpPattern());
            lateralRaisePattern.onActionDone += onRepetitionDoneHandler;

        }

        public override void ProcessData(double x, double y, double z)
        {
            lateralRaisePattern.processAccelData(x, y, z);
        }
    }
}

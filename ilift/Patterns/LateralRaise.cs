using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    public class LateralRaise : AbstractExercise
    {
        public event RepetitionDelegate onRepetitionDone;
        private ComplexPattern lateralRaisePattern;

        public LateralRaise()
        {
            lateralRaisePattern = new ComplexPattern();
            lateralRaisePattern.addPattern(new LateralRaiseDownPattern());
            lateralRaisePattern.addPattern(new LateralRaiseUpPattern());
            lateralRaisePattern.onActionDone += () => onRepetitionDone();

        }

        public void ProcessData(double x, double y, double z)
        {
            lateralRaisePattern.processAccelData(x, y, z);
        }
    }
}

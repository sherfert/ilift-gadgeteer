using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    class LateralRaise
    {
        public event ActionDelegate onRepetitionDone;
        private ComplexPattern lateralRaisePattern;
        public LateralRaise()
        {
            lateralRaisePattern = new ComplexPattern();
            lateralRaisePattern.addPattern(new LateralRaiseDownPattern());
            lateralRaisePattern.addPattern(new LateralRaiseUpPattern());
            lateralRaisePattern.onActionDone += repetition;

        }

        public void processData(double x, double y, double z)
        {
            lateralRaisePattern.processAccelData(x, y, z);
        }

        private void repetition()
        {
            onRepetitionDone();
        }
    }
}

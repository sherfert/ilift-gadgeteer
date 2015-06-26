using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    public class TricepExtension:AbstractExercise
    {
        private ComplexPattern tricepExtensionPattern;

        public TricepExtension()
        {
            tricepExtensionPattern = new ComplexPattern();
            tricepExtensionPattern.addPattern(new TricepExtensionUpPattern());
            tricepExtensionPattern.addPattern(new TricepExtensionDownPattern());
            tricepExtensionPattern.onActionDone += onRepetitionDoneHandler;

        }

        public override void ProcessData(double x, double y, double z)
        {
            tricepExtensionPattern.processAccelData(x, y, z);
        }
    }
}

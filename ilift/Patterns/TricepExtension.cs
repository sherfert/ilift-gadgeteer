using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;

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
            tricepExtensionPattern.onSubPatternDone += onSubPatternDoneHandler;

        }

        public override void ProcessData(double x, double y, double z)
        {
            tricepExtensionPattern.processAccelData(x, y, z);
        }
    }
}

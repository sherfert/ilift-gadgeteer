using System;
using Microsoft.SPOT;
using System.Collections;

namespace ilift.Patterns
{
    class ComplexPattern:IActionPattern
    {
        private ArrayList subPatterns;
        private IActionPattern currentPattern;

        public event ActionDelegate onActionDone;

        public ComplexPattern()
        {
            subPatterns = new ArrayList();
        }

        public void processAccelData(double x, double y, double z)
        {
            if(currentPattern != null)
                currentPattern.processAccelData(x, y, z);
        }

        public void addPattern(IActionPattern pattern)
        {
            pattern.onActionDone += nextPattern;
            subPatterns.Add(pattern);
            currentPattern = (IActionPattern)subPatterns[0];

        }

        private void nextPattern()
        {
            int next = subPatterns.IndexOf(currentPattern) + 1;
            if (next < subPatterns.Count)
                currentPattern = (IActionPattern)subPatterns[next];
            else
            {
                repetitionDone();
            }
        }

        private void repetitionDone()
        {
            currentPattern = (IActionPattern)subPatterns[0];
            onActionDone();
        }
    }
}

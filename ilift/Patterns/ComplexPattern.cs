using System;
using Microsoft.SPOT;
using System.Collections;

namespace ilift.Patterns
{
    /// <summary>
    /// Class for complex pattern, which is composed of patterns
    /// </summary>
    public class ComplexPattern:IActionPattern
    {
        private ArrayList subPatterns;
        private IActionPattern currentPattern;

        public event ActionDelegate onActionDone;
        public event ActionDelegate onSubPatternDone;

        public ComplexPattern()
        {
            subPatterns = new ArrayList();
        }

        /// <summary>
        /// Passes the accelerometer arguments to the current pattern.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void processAccelData(double x, double y, double z)
        {
            if (currentPattern != null)
            {
                Debug.Print("Accelerometer:\tx: " + x + "\ty: " + y + "\tz: " + z + "\n");
                currentPattern.processAccelData(x, y, z);
            }
        }

        /// <summary>
        /// Method for adding pattern to pattern list and adding nextPattern Handler to it.
        /// </summary>
        /// <param name="pattern"></param>
        public void addPattern(IActionPattern pattern)
        {
            pattern.onActionDone += nextPattern;
            subPatterns.Add(pattern);
            currentPattern = (IActionPattern)subPatterns[0];

        }

        /// <summary>
        /// Chains patterns by going to the next pattern in the pattern array list.
        /// If pattern is last fire repetition done.
        /// </summary>
        /// <param name="actionQuality"></param>
        /// <param name="msg"></param>
        private void nextPattern(Quality actionQuality, String msg)
        {
            onSubPatternDone(actionQuality, msg);
            int next = subPatterns.IndexOf(currentPattern) + 1;
            if (next < subPatterns.Count)
                currentPattern = (IActionPattern)subPatterns[next];
            else
            {
                repetitionDone();
            }
        }
        /// <summary>
        /// Called when repetition is done, set the first pattern as the current pattern.
        /// The arguement passed in actionDone is disregarded as only sub patterns quality is evaluated.
        /// 
        /// </summary>
        private void repetitionDone()
        {
            currentPattern = (IActionPattern)subPatterns[0];            
            onActionDone(Quality.BAD,"");
        }
    }
}

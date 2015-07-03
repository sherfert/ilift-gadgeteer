using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    public enum Quality
    {
        GOOD,
        BAD
    }
    /// <summary>
    /// Action delegate that is called when pattern is recognised
    /// </summary>
    /// <param name="quality"></param>
    /// <param name="msg"></param>
    public delegate void ActionDelegate(Quality quality, String msg);
    /// <summary>
    /// Interface class for all patterns
    /// </summary>
    public interface IActionPattern
    {
        event ActionDelegate onActionDone;
       /// <summary>
       /// Passed the arguements from the accelometer and checks if pattern is recognised
       /// </summary>
       /// <param name="x"></param>
       /// <param name="y"></param>
       /// <param name="z"></param>
        void processAccelData(double x, double y, double z);
    }
}

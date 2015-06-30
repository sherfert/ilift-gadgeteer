using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    public enum Quality
    {
        GOOD,
        BAD
    }

    public delegate void ActionDelegate(Quality quality, String msg);
 
    public interface IActionPattern
    {
        event ActionDelegate onActionDone;
       
        void processAccelData(double x, double y, double z);
    }
}

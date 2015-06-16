using System;
using Microsoft.SPOT;

namespace ilift.Patterns
{
    public delegate void ActionDelegate();
 
    public interface IActionPattern
    {
        event ActionDelegate onActionDone;
        void processAccelData(double x, double y, double z);
    }
}

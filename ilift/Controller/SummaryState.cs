using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;

namespace ilift.Controller
{
    public class SummaryState : ExecutionState
    {

        public SummaryState(DisplayTE35 display, StateManager state)
            : base(display, state)
        {
            
        }

        public override void init()
        {
            display.WPFWindow.UpdateLayout(); //TODO do we need this?
            Debug.Print("Summary State init");
        }

        public override void finish()
        {
            Debug.Print("Finish Summary state");
        }
    }
}

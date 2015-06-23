using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;
using Microsoft.SPOT.Presentation.Controls;

namespace ilift.Controller
{
    /** An abstract class for all states of the mastermind application.
     */
    public abstract class ExecutionState
    {
        protected DisplayTE35 display;
        protected StateManager stateManager;
        protected Canvas canvas;

        /** The protected constructor needs the display to draw gui elements on
         * and a reference to the state manager.
         */
        protected ExecutionState(DisplayTE35 display, StateManager state) 
        {
            this.display = display;
            this.stateManager = state;
            canvas = new Canvas();
        }

        /** Initializes the state by registering all event handlers.
         */
        public abstract void init();

        /** Cleans the state by unregistering all event handlers.
         */
        public abstract void finish();
    }
}

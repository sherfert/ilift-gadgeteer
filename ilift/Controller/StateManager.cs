using System;
using Gadgeteer.Modules.GHIElectronics;
using ilift.Model;
using Microsoft.SPOT;

namespace ilift.Controller
{
    // Delegate for all events thos interface defines.
    public delegate void GUIDelegate();

    // TODO add comments
    public delegate void GUITouchDelegate(int x, int y);

    // TODO add comments
    public delegate void RFIDReadDelegate(String tag);

    /** This interface defines logical GUI events (like moving up or down)
     * that can be triggered by the hardware.
     */
    public interface StateManager
    {
        // Event that is fired when the main button is clicked
        event GUIDelegate OnMainButtonClick;
        
        // Event that is fired whenever the screen is touched
        event GUITouchDelegate OnScreenTouched;

        event RFIDReadDelegate OnCardRead;

        /** Switch to a new state
         */
        void SwitchState(ExecutionState newState);

        HardwareController GetHardwareController();
       

        Session GetSession();
    }
}

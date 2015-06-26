using System;
using Gadgeteer.Modules.GHIElectronics;
using ilift.Model;
using Microsoft.SPOT;

namespace ilift.Controller
{
    // Delegate for all events thos interface defines.
    public delegate void GUIDelegate();

    //The delegate that contains the rfid tag in the signature
    public delegate void RFIDReadDelegate(String tag);

    /** This interface defines logical GUI events (like moving up or down)
     * that can be triggered by the hardware.
     */
    public interface StateManager
    {
        // Event that is fired when the main button is clicked
        event GUIDelegate OnMainButtonClick;
        
        event RFIDReadDelegate OnCardRead;

        /** Switch to a new state
         */
        void SwitchState(ExecutionState newState);
        
        /// <summary>
        /// returns the HardwareController
        /// </summary>
        /// <returns></returns>
        HardwareController GetHardwareController();
       
        /// <summary>
        /// returns Session which stores the information throughout the application
        /// </summary>
        /// <returns></returns>
        Session GetSession();
    }
}

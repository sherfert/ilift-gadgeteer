using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;
using ilift.Controller;

namespace ilift
{
    // Callback for joystick movement
    public delegate void Callback();

    /// <summary>
    /// A controller for the gadgeteer hardware
    /// </summary>
    public interface HardwareController
    {
        /// TODO Add the events for the touch screen
        /// 
                
        /// <summary>
        /// Register a handler for button pressed
        /// </summary>
        /// <param name="downHandler">the handler</param>
       // void RegisterButtonPressedHandler(Button.ButtonEventHandler handler);

        void RegisterDisplayTouchedHandler(/*TODO correct params*/GUITouchDelegate handler);

        void RegisterRFIDReadHandler(RFIDReader.IdReceivedEventHandler handler);

        /// <summary>
        /// Get the display
        /// </summary>
        /// <returns>the display</returns>
        DisplayTE35 GetDisplay();
        
        /// <summary>
        /// Get the accelerometer
        /// </summary>
        /// <returns></returns>
        Accelerometer GetAccelerometer();

        /// <summary>
        /// Get the compass
        /// </summary>
        /// <returns></returns>
        Compass GetCompass();
    }
}

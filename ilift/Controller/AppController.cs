using System;
using Gadgeteer.Modules.GHIElectronics;
using ilift.Model;
using Microsoft.SPOT;

namespace ilift.Controller
{
    /** The game controller maps actual hardware events to logical GUI events.
     * It therefore implements the StateManager interface.
     * 
     * It is responsible for managing the current state and changing states.
     */
    public class AppController : StateManager
    {
        // Constants for GUI placements
        public const int ROW_1_Y = 20;
        public const int COL_1_X = 15;
        public const int ROW_2_Y = 40;

        // The current state
        private ExecutionState state;

        // Inherited events
        public event GUIDelegate OnMainButtonClick;
        public event GUITouchDelegate OnScreenTouched;
        public event RFIDReadDelegate OnCardRead;

        // The hardware controller
        private HardwareController _hardwareController;

        //Session 
        private Session _session = new Session();

        public HardwareController HardwareController
        {
            get { return _hardwareController; }
            set { _hardwareController = value; }
        }

        public Session Session
        {
            get { return _session; }
            set { _session = value; }
        }

        /** Constructor that accepts a hardware controller
         */
        public AppController(HardwareController hardwareController)
        {
            this._hardwareController = hardwareController;

            // Register at the hardware controller, so that the logical events
            // are fired, if the corresponsing hardware events are triggered.
            this._hardwareController.RegisterButtonPressedHandler((sender, buttonState) 
                => { if (OnMainButtonClick!=null) OnMainButtonClick(); });
            this._hardwareController.RegisterDisplayTouchedHandler((x, y)
                => { if (OnScreenTouched != null) OnScreenTouched(x, y); });
            this._hardwareController.RegisterRFIDReadHandler((device, tag)
                => { if (OnCardRead != null) OnCardRead(tag); });
            // Switch the state to the first state in the game: GameModeChoosingState
            SwitchState(new WelcomeState(hardwareController.GetDisplay(),this));
        }

        /*
         * Switches the state.
         */
        public void SwitchState(ExecutionState newState)
        {
            if (state != null)
            {
                state.finish();
            }
            // Clear display for new state
            _hardwareController.GetDisplay().SimpleGraphics.Clear();

            state = newState;
            state.init();
        }

        public Session GetSession()
        {
            return _session;
        }

        public Accelerometer GetAccelerometer()
        {
            return _hardwareController.GetAccelerometer();
        }

        public Compass GetCompass()
        {
            return _hardwareController.GetCompass();
        }
    }
}

using System;
using Gadgeteer.Modules.GHIElectronics;
using ilift.Model;
using Microsoft.SPOT;

namespace ilift.Controller
{
    /// <summary>
    /// The game controller maps actual hardware events to logical GUI events.
    /// It therefore implements the StateManager interface. 
    /// It is responsible for managing the current state and changing states.
    /// </summary>
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
        public event RFIDReadDelegate OnCardRead;

        // The hardware controller
        private HardwareController _hardwareController;

        //Session 
        private Session _session = new Session();

        /// <summary>
        /// the hardware controller 
        /// </summary>
        public HardwareController HardwareController
        {
            get { return _hardwareController; }
            set { _hardwareController = value; }
        }

        public HardwareController GetHardwareController()
        {
            return _hardwareController;
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

        /// <summary>
        /// Session object that persist through the different states
        /// </summary>
        /// <returns></returns>
        public Session GetSession()
        {
            return _session;
        }

        /// <summary>
        /// Access Accelerometer Hardware
        /// </summary>
        /// <returns>Accelerometer</returns>
        public Accelerometer GetAccelerometer()
        {
            return _hardwareController.GetAccelerometer();
        }

        /// <summary>
        /// Access Compass Hardware
        /// </summary>
        /// <returns>Compass</returns>
        public Compass GetCompass()
        {
            return _hardwareController.GetCompass();
        }
        
        /// <summary>
        /// Access Tunes Hardware
        /// </summary>
        /// <returns>Tunes</returns>
        public Tunes GetTunes()
        {
            return _hardwareController.GetTunes();
        }
    }
}

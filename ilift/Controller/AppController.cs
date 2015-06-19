using System;
using Gadgeteer.Modules.GHIElectronics;
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
        private HardwareController hardwareController;

        public HardwareController HardwareController
        {
            get { return hardwareController; }
            set { hardwareController = value; }
        }

        /** Constructor that accepts a hardware controller
         */
        public AppController(HardwareController hardwareController)
        {
            this.hardwareController = hardwareController;

            // Register at the hardware controller, so that the logical events
            // are fired, if the corresponsing hardware events are triggered.
            this.hardwareController.RegisterButtonPressedHandler((sender, buttonState) 
                => { if (OnMainButtonClick!=null) OnMainButtonClick(); });
            this.hardwareController.RegisterDisplayTouchedHandler((x, y)
                => { if (OnScreenTouched != null) OnScreenTouched(x, y); });
            this.hardwareController.RegisterRFIDReadHandler((device, tag)
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
            hardwareController.GetDisplay().SimpleGraphics.Clear();

            state = newState;
            state.init();
        }

        public Accelerometer GetAccelerometer()
        {
            return hardwareController.GetAccelerometer();
        }

        public Compass GetCompass()
        {
            return hardwareController.GetCompass();
        }
    }
}

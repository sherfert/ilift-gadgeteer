using System;
using Microsoft.SPOT;
using ilift.GUI;
using Gadgeteer.Modules.GHIElectronics;

namespace ilift.Controller
{
    /// <summary>
    /// The state to choose between 1player or 2 player mode.
    /// </summary>
    public class WelcomeState : ExecutionState
    {
        private const string WELCOME_TEXT = "Welcome to iLift!";
        private const string SCAN_YOUR_CARD_TEXT = "Scan your card";
        private const string ERROR_TEXT = "Unknown user";

        private GString _welcomeLabel;
        private GString _scanYourCardLabel;
        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="display">the display</param>
        /// <param name="stateManager">the state manager</param>
        public WelcomeState(DisplayTE35 display, StateManager stateManager) : base(display, stateManager)
        {
        }

        override public void init()
        {
            // Init GUI
            _welcomeLabel = new GString(AppController.COL_1_X, AppController.ROW_1_Y, WELCOME_TEXT);
            _scanYourCardLabel = new GString(AppController.COL_1_X, AppController.ROW_2_Y, SCAN_YOUR_CARD_TEXT);

            stateManager.OnCardRead += BindUser;                  
            
            Redraw();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        private void BindUser(string tag)
        {
            Debug.Print("Scanned tag: "+tag);
            //throw new NotImplementedException();
        }

        
        override public void finish()
        {
            stateManager.OnCardRead -= BindUser; 
        }
        
        /// <summary>
        /// Redraws the GUI.
        /// </summary>
        private void Redraw()
        {
            _welcomeLabel.draw(display);
            _scanYourCardLabel.draw(display);
        }
    }
}

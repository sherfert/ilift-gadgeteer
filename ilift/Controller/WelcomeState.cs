using System;
using Microsoft.SPOT;
using ilift.GUI;
using Gadgeteer.Modules.GHIElectronics;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Presentation.Media;
using ilift.Network;

namespace ilift.Controller
{
    /// <summary>
    /// Welcome States promts the user to scan his/her id card.
    /// </summary>
    public class WelcomeState : ExecutionState
    {
        private const string WELCOME_TEXT = "Welcome to iLift!";
        private const string SCAN_YOUR_CARD_TEXT = "Scan your card";
        private const string ERROR_TEXT = "Scan a valid user card";

        private Text _welcomeLabel;
        private Text _scanYourCardLabel;

        /// <summary>
        /// Constructing the WelcomeState passing display, and stateManager to the super
        /// </summary>
        /// <param name="display">the display</param>
        /// <param name="stateManager">the state manager</param>
        public WelcomeState(DisplayTE35 display, StateManager stateManager)
            : base(display, stateManager)
        {
        }

        override public void init()
        {
            // Init GUI
            display.WPFWindow.UpdateLayout();
            //Font font = Resources.GetFont(Resources.FontResources.NinaB);
            Font font = Resources.GetFont(Resources.FontResources.welcome_font);
            _welcomeLabel = new Text(font, WELCOME_TEXT);
            _welcomeLabel.ForeColor = Gadgeteer.Color.Black;
            Canvas.SetTop(_welcomeLabel, 50);
            Canvas.SetLeft(_welcomeLabel, 80);

            _scanYourCardLabel = new Text(font, SCAN_YOUR_CARD_TEXT);
            _scanYourCardLabel.ForeColor = Gadgeteer.Color.Red;
            Canvas.SetTop(_scanYourCardLabel, 100);
            Canvas.SetLeft(_scanYourCardLabel, 80);

            canvas.Children.Add(_welcomeLabel);
            canvas.Children.Add(_scanYourCardLabel);

            display.WPFWindow.Child = canvas;

            // INIT EVENTS
            stateManager.OnCardRead += BindUser;
            
        }

        /// <summary>
        /// The handler of RfidReader, it binds the user to the current session 
        /// and changes the state to select equipment state. 
        /// </summary>
        /// <param name="tag">the tag passed to the handler</param>
        private void BindUser(string tag)
        {
            Debug.Print("Scanned tag: " + tag);
            NetworkClient.GetUser(tag, user =>
            {
                if (user != null)
                {
                    stateManager.GetSession().User = user;
                    stateManager.SwitchState(new SelectEquipmentState(display, stateManager));
                }
                else
                {
                    _scanYourCardLabel.TextContent = ERROR_TEXT;
                }
            });
        }

        override public void finish()
        {
            stateManager.OnCardRead -= BindUser;
        }
    }
}

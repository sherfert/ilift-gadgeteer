using System;
using Gadgeteer.Modules.GHIElectronics;
using ilift.GUI;
using ilift.Network;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;

namespace ilift.Controller
{
    /// <summary>
    /// State where the user is promt to scan an equipment
    /// </summary>
    public class SelectEquipmentState : ExecutionState
    {
        private const string WELCOME_TEXT = "Welcome ";
        private const string SCAN_AN_EQUIPMENT_TEXT = "Scan an Equipment";
        private const string ERROR_TEXT = "Scan a valid equipment";
        private const string LOGOUT_TEXT = "Logout";

        private Text _welcomeLabel;
        private Text _scanAnEquipmentLabel;

        private ParameterizedRectangle _logoutButton;
        private Text _logoutLabel;

        /// <summary>
        /// SelectEquipmentState deals with scaning the equipment rfid tag and switching to
        /// the state where the user is promt to select the exercise.
        /// </summary>
        /// <param name="display">the display</param>
        /// <param name="stateManager">the state manager</param>
        public SelectEquipmentState(DisplayTE35 display, StateManager state) : base(display, state)
        {
        }


        public override void init()
        {
            //display.WPFWindow.UpdateLayout(); //TODO do we need this ?
            Font font = Resources.GetFont(Resources.FontResources.welcome_font);
            //first welcome label on the screen 
            _welcomeLabel = new Text(GUIConstants.FONT, WELCOME_TEXT + stateManager.GetSession().User.username);
            _welcomeLabel.ForeColor = Gadgeteer.Color.Black;
            
            //position the label on the screen
            Canvas.SetTop(_welcomeLabel, 50);
            Canvas.SetLeft(_welcomeLabel, 100);

            // the same thing for the second message
            _scanAnEquipmentLabel = new Text(font, SCAN_AN_EQUIPMENT_TEXT);
            _scanAnEquipmentLabel.ForeColor = Gadgeteer.Color.Red;
            Canvas.SetTop(_scanAnEquipmentLabel, 100);
            Canvas.SetLeft(_scanAnEquipmentLabel, 70);

            canvas.Children.Add(_welcomeLabel);
            canvas.Children.Add(_scanAnEquipmentLabel);


            int buttonHeight = (int)(display.Height * GUIConstants.HEIGHT_PERCENTAGE);
            int bigButtonWidth = (int)(display.Width * GUIConstants.BIG_BUTTON_WIDTH_PERCENTAGE);
            int startY = 2 * buttonHeight;

            // Logout button
            _logoutButton = new ParameterizedRectangle(bigButtonWidth, buttonHeight);
            _logoutButton.Fill = new SolidColorBrush(GUIConstants.SPECIAL_BUTTON_COLOR);

            Canvas.SetTop(_logoutButton, startY + 2 * (buttonHeight + GUIConstants.DEFAULT_SPACING));
            Canvas.SetLeft(_logoutButton, display.Width / 2 - (bigButtonWidth / 2));

            _logoutLabel = new Text(GUIConstants.FONT, LOGOUT_TEXT);
            _logoutLabel.ForeColor = GUIConstants.TEXT_COLOR;
            Canvas.SetTop(_logoutLabel, startY + buttonHeight / 2 +
                2 * (buttonHeight + GUIConstants.DEFAULT_SPACING) - GUIConstants.DEFAULT_MARGIN);
            Canvas.SetLeft(_logoutLabel, GUIConstants.LEFT_OFFSET + GUIConstants.LOWER_BUTTON_LABEL_OFFSET);

            _logoutButton.TouchDown += OnLogoutClicked;
            _logoutLabel.IsEnabled = false;

            canvas.Children.Add(_logoutButton);
            canvas.Children.Add(_logoutLabel);
            
            display.WPFWindow.Child = canvas;

            // here we subscribe to the OnCardRead event 
            // from the hardware and do the processing of the rfid tag in
            // BindEquipment
            stateManager.OnCardRead += BindEquipment;
        }

        /// <summary>
        /// Handler to Rectangle LogOutClick Touch Down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLogoutClicked(object sender, Microsoft.SPOT.Input.TouchEventArgs e)
        {
            stateManager.SwitchState(new WelcomeState(display, stateManager));
        }

        /// <summary>
        /// Handler to rfid reader, bindes the equipment to the current session
        /// and changes the state to promt the user to select the exercise for the given equipment.
        /// </summary>
        /// <param name="tag">Rfid Tag</param>
        private void BindEquipment(string tag)
        {
            NetworkClient.GetEquipmentByTag(tag, equipment =>
            {
                //if equipment is not null continue, otherwise display an error message 
                //in the current screen.
                if (equipment != null)
                {
                    stateManager.GetSession().Equipment = equipment;
                    stateManager.SwitchState(new SelectExerciseState(display, stateManager));
                }
                else
                {
                    _scanAnEquipmentLabel.TextContent = ERROR_TEXT;
                }
            });
        }

        public override void finish()
        {
            stateManager.OnCardRead -= BindEquipment;
        }
    }
}

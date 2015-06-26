using System;
using Gadgeteer.Modules.GHIElectronics;
using ilift.GUI;
using ilift.Network;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;

namespace ilift.Controller
{
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

        public SelectEquipmentState(DisplayTE35 display, StateManager state) : base(display, state)
        {
        }

        public override void init()
        {
            display.WPFWindow.UpdateLayout();
            _welcomeLabel = new Text(GUIConstants.FONT, WELCOME_TEXT + stateManager.GetSession().User.username);
            _welcomeLabel.ForeColor = Gadgeteer.Color.Black;
            Canvas.SetTop(_welcomeLabel, 50);
            Canvas.SetLeft(_welcomeLabel, 100);

            _scanAnEquipmentLabel = new Text(GUIConstants.FONT, SCAN_AN_EQUIPMENT_TEXT);
            _scanAnEquipmentLabel.ForeColor = Gadgeteer.Color.Red;
            Canvas.SetTop(_scanAnEquipmentLabel, 100);
            Canvas.SetLeft(_scanAnEquipmentLabel, 100);

            canvas.Children.Add(_welcomeLabel);
            canvas.Children.Add(_scanAnEquipmentLabel);


            int buttonHeight = (int)(display.Height * GUIConstants.HEIGHT_PERCENTAGE);
            int bigButtonWidth = (int)(display.Width * GUIConstants.BIG_BUTTON_WIDTH_PERCENTAGE);
            int startY = 2 * buttonHeight;

            // Logout button
            _logoutButton = new ParameterizedRectangle(bigButtonWidth, buttonHeight);
            _logoutButton.Fill = new SolidColorBrush(GUIConstants.SPECIAL_BUTTON_COLOR);

            _logoutButton.SetMargin(GUIConstants.DEFAULT_MARGIN);
            Canvas.SetTop(_logoutButton, startY + 2 * (buttonHeight + GUIConstants.DEFAULT_SPACING));
            Canvas.SetLeft(_logoutButton, GUIConstants.LEFT_OFFSET);

            _logoutLabel = new Text(GUIConstants.FONT, LOGOUT_TEXT);
            _logoutLabel.ForeColor = GUIConstants.TEXT_COLOR;
            Canvas.SetTop(_logoutLabel, startY + buttonHeight / 2 +
                2 * (buttonHeight + GUIConstants.DEFAULT_SPACING));
            Canvas.SetLeft(_logoutLabel, GUIConstants.LEFT_OFFSET + GUIConstants.LOWER_BUTTON_LABEL_OFFSET);

            _logoutButton.TouchDown += OnLogoutClicked;

            canvas.Children.Add(_logoutButton);
            canvas.Children.Add(_logoutLabel);
            
            display.WPFWindow.Child = canvas;

            //Events 
            stateManager.OnCardRead += BindEquipment;
        }

        private void OnLogoutClicked(object sender, Microsoft.SPOT.Input.TouchEventArgs e)
        {
            stateManager.SwitchState(new WelcomeState(display, stateManager));
        }

        private void BindEquipment(string tag)
        {
            NetworkClient.GetEquipmentByTag(tag, equipment =>
            {
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

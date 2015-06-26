using System;
using System.Text;
using ilift.GUI;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;

namespace ilift.Controller
{
    public class SummaryState : ExecutionState
    {

        public const string MESSAGE = "Session finished!";
        public const string SUMMARY_START_TEXT = "You did: ";
        public const string CONTINUE_BUTTON_TEXT = "Continue";

        private Text _messageLabel;
        private Text _summaryLabel;

        private ParameterizedRectangle _continueButton;
        private Text _continueLabel;

        public SummaryState(DisplayTE35 display, StateManager state)
            : base(display, state)
        {
            
        }

        public override void init()
        {
            display.WPFWindow.UpdateLayout();
            _messageLabel = new Text(GUIConstants.FONT, MESSAGE);
            _messageLabel.ForeColor = Gadgeteer.Color.Black;
            Canvas.SetTop(_messageLabel, 50);
            Canvas.SetLeft(_messageLabel, 25);

            StringBuilder sb = new StringBuilder();
            sb.Append(SUMMARY_START_TEXT);
            sb.Append(stateManager.GetSession().Repetitions).Append(" ");
            sb.Append(stateManager.GetSession().Exercise.Name).Append(", ");
            sb.Append(stateManager.GetSession().Equipment.Type.Name).Append(", ");
            sb.Append(stateManager.GetSession().Equipment.WeightKg).Append(" kg.");
            
            _summaryLabel = new Text(GUIConstants.FONT, sb.ToString());
            _summaryLabel.ForeColor = Gadgeteer.Color.Green;
            Canvas.SetTop(_summaryLabel, 100);
            Canvas.SetLeft(_summaryLabel, 25);

            canvas.Children.Add(_messageLabel);
            canvas.Children.Add(_summaryLabel);


            int buttonHeight = (int)(display.Height * GUIConstants.HEIGHT_PERCENTAGE);
            int bigButtonWidth = (int)(display.Width * GUIConstants.BIG_BUTTON_WIDTH_PERCENTAGE);
            int startY = 2 * buttonHeight;

            // Continue button
            _continueButton = new ParameterizedRectangle(bigButtonWidth, buttonHeight);
            _continueButton.Fill = new SolidColorBrush(GUIConstants.NORMAL_BUTTON_COLOR);

            _continueButton.SetMargin(GUIConstants.DEFAULT_MARGIN);
            Canvas.SetTop(_continueButton, startY + 2 * (buttonHeight + GUIConstants.DEFAULT_SPACING));
            Canvas.SetLeft(_continueButton, GUIConstants.LEFT_OFFSET);

            _continueLabel = new Text(GUIConstants.FONT, CONTINUE_BUTTON_TEXT);
            _continueLabel.ForeColor = GUIConstants.TEXT_COLOR;
            Canvas.SetTop(_continueLabel, startY + buttonHeight / 2 +
                2 * (buttonHeight + GUIConstants.DEFAULT_SPACING));
            Canvas.SetLeft(_continueLabel, GUIConstants.LEFT_OFFSET + GUIConstants.LOWER_BUTTON_LABEL_OFFSET);

            _continueButton.TouchDown += OnContinueClicked;

            canvas.Children.Add(_continueButton);
            canvas.Children.Add(_continueLabel);

            display.WPFWindow.Child = canvas;


        }

        private void OnContinueClicked(object sender, TouchEventArgs e)
        {
            stateManager.SwitchState(new SelectEquipmentState(display,stateManager));
        }

        public override void finish()
        {
        }
    }
}

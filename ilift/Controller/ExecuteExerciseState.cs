using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;
using ilift.Patterns;
using Microsoft.SPOT.Presentation.Controls;
using ilift.GUI;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Input;
using ilift.Model;

namespace ilift.Controller
{
    public class ExecuteExerciseState:ExecutionState
    {
        protected AbstractExercise exercise;

        private const int SPACING = 15;
        private const int DONE_LABEL_OFFSET = 120;
        private readonly Color DONE_BUTTON_COLOR = Gadgeteer.Color.Red;
        private const String DONE_TEXT = "Done";
        private const int LEFT_OFFSET = 10;
        // For exercise state
        protected Text _repNumber;
        protected int count;

        private ParameterizedRectangle _doneButton;
        private Text _doneLabel;

        public ExecuteExerciseState(DisplayTE35 display, StateManager state, AbstractExercise exercise)
            : base(display, state)
        {
            this.exercise = exercise;
        }

        // Put all Gui drawing init codes here
        public override void init()
        {
            count = 0;
            int buttonWidth = (int)(display.Width * 0.40);
            int buttonHeight = (int)(display.Height * 0.15);
            int startY = 2 * buttonHeight;
            int startX = LEFT_OFFSET;
            display.WPFWindow.UpdateLayout(); //TODO do we need this?
            Font font = Resources.GetFont(Resources.FontResources.NinaB);
            exercise.onRepetitionDone += UpdateScreen;
            exercise.StartExercise();
            _repNumber = new Text(font, "0");
            Canvas.SetTop(_repNumber, display.Height / 3);
            Canvas.SetLeft(_repNumber, display.Width / 2);

            // Create a cancel button
            _doneButton = new ParameterizedRectangle(2 * buttonWidth + SPACING, buttonHeight);
            _doneButton.Fill = new SolidColorBrush(DONE_BUTTON_COLOR);

           _doneButton.SetMargin(GUI.GUIConstants.DEFAULT_MARGIN);
            Canvas.SetTop(_doneButton, startY + 2 * (buttonHeight + SPACING));
            Canvas.SetLeft(_doneButton, startX);

            _doneLabel = new Text(font, DONE_TEXT);
            _doneLabel.ForeColor = Gadgeteer.Color.Black;
            Canvas.SetTop(_doneLabel, startY + buttonHeight / 2 +
                2 * (buttonHeight + SPACING));
            Canvas.SetLeft(_doneLabel, startX + DONE_LABEL_OFFSET);

            _doneButton.TouchDown += OnDoneClicked;

            canvas.Children.Add(_doneButton);
            canvas.Children.Add(_doneLabel);

            canvas.Children.Add(_repNumber);
            display.WPFWindow.Child = canvas;
        }

        // TODO post the result to web server
        private void OnDoneClicked(object sender, TouchEventArgs touchEventArgs)
        {
            Debug.Print("Touched");
            exercise.StopExercise();

            if (count != 0)
            {
                Session session = stateManager.GetSession();
                session.Repetitions = count;
                Network.NetworkClient.PostSession(session);
            }
            else
            {
                stateManager.SwitchState(new SelectExerciseState(display, stateManager));
            }
            //stateManager.SwitchState(new SummaryState(display, stateManager));
        }

        public void UpdateScreen()
        {
            count++;
            _repNumber.TextContent = count.ToString();
        }

        public override void finish()
        {
            _doneButton.TouchDown -= OnDoneClicked;    
        }
    }
}

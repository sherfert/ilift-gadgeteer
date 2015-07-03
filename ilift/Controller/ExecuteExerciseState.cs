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
        protected Text _message;
        protected int _repetitions;
        protected int _badRepetitions;

        private ParameterizedRectangle _doneButton;
        private Text _doneLabel;

        /// <summary>
        /// Constructs the state that will 
        /// </summary>
        /// <param name="display"></param>
        /// <param name="state"></param>
        /// <param name="exercise"></param>
        public ExecuteExerciseState(DisplayTE35 display, StateManager state, AbstractExercise exercise)
            : base(display, state)
        {
            this.exercise = exercise;
        }

        // Put all Gui drawing init codes here
        public override void init()
        {
            _repetitions = 0;
            _badRepetitions = 0;
            int buttonWidth = (int)(display.Width * 0.40);
            int buttonHeight = (int)(display.Height * 0.15);
            int startY = 2 * buttonHeight;
            int startX = LEFT_OFFSET;
            
            Font font = Resources.GetFont(Resources.FontResources.NinaB);
            
            //here we register the handler to update the screen 
            exercise.onRepetitionDone += UpdateScreen;
            exercise.onSubPatternDone += UpdateMessage;
            exercise.StartExercise();

            _repNumber = new Text(font, "0");
            _message = new Text(font, "");
            _message.ForeColor = Gadgeteer.Color.Red;
            Canvas.SetTop(_repNumber, display.Height / 3);
            Canvas.SetLeft(_repNumber, display.Width / 2);

            Canvas.SetTop(_message, (display.Height / 3) + 20);
            Canvas.SetLeft(_message, (display.Width / 2) - 20);

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
            canvas.Children.Add(_message);
            display.WPFWindow.Child = canvas;
        }

        private void UpdateMessage(string msg)
        {
            _message.TextContent = msg;
        }

        /// <summary>
        /// The handler of rectangle Done when clicked session is saved
        /// and state is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="touchEventArgs"></param>
        private void OnDoneClicked(object sender, TouchEventArgs touchEventArgs)
        {
            Debug.Print("Touched");
            exercise.StopExercise();

            if (_repetitions + _badRepetitions != 0)
            {
                Session session = stateManager.GetSession();
                session.Repetitions = _repetitions;
                session.BadRepetitions = _badRepetitions;
                Network.NetworkClient.PostSession(session);
                stateManager.SwitchState(new SummaryState(display, stateManager));
            }
            else
            {
                stateManager.SwitchState(new SelectExerciseState(display, stateManager));
            }
            
        }


        /// <summary>
        /// Whenever one repetition is done this handler is fired
        /// </summary>
        public void UpdateScreen(Boolean hasGoodQuality)
        {

            if (hasGoodQuality)
            {
                _repetitions++;
            }
            else
            {
                _badRepetitions++;
            }
           
           _repNumber.TextContent = (_repetitions + _badRepetitions).ToString();
        }

        public override void finish()
        {
            _doneButton.TouchDown -= OnDoneClicked;    
        }
    }
}

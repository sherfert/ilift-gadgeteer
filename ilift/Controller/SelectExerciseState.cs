using System;
using System.Text;
using Gadgeteer.Modules.GHIElectronics;
using ilift.GUI;
using ilift.Model;
using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;

namespace ilift.Controller
{
    /// <summary>
    /// In this state the exercise for a chosen equipment can be selected.
    /// </summary>
    class SelectExerciseState : ExecutionState
    {
        private const String SELECTED_EQUIPEMENT_TEXT = "Selected equipment: ";
        private const String CHOOSE_EXERCISE_TEXT = "Choose an exercise";
        private const String CANCEL_TEXT = "Cancel";
        private const String EXERCISE_KEY = "exercise";

        private const int MAX_EXERCISE_BUTTONS = 4;

        private Text _selectedEquipment;
        private Text _chooseExercise;

        private ParameterizedRectangle[] exButtons = new ParameterizedRectangle[4];
        private Text[] buttonLabels = new Text[4];
        private ParameterizedRectangle _cancelButton;
        private Text _cancelLabel;

        /// <summary>
        /// SelectExerciseState promts the user to select an exercise according to the
        /// equipment selected. 
        /// </summary>
        /// <param name="display">the display</param>
        /// <param name="stateManager">the state manager</param>
        public SelectExerciseState(DisplayTE35 display, StateManager state)
            : base(display, state)
        {

        }

        public override void init()
        {
            // Get all available exercises for that equipment
            Exercise[] exercises = stateManager.GetSession().Equipment.Type.AvailableExercises;
           
            _selectedEquipment = new Text(GUIConstants.FONT, SELECTED_EQUIPEMENT_TEXT + stateManager.GetSession().Equipment.Type.Name);
            _selectedEquipment.ForeColor = Gadgeteer.Color.Green;
            canvas.Children.Add(_selectedEquipment);
            Canvas.SetTop(_selectedEquipment, 20);
            Canvas.SetLeft(_selectedEquipment, GUIConstants.LEFT_OFFSET);

            _chooseExercise = new Text(GUIConstants.FONT, CHOOSE_EXERCISE_TEXT);
            _chooseExercise.ForeColor = Gadgeteer.Color.Black;
            canvas.Children.Add(_chooseExercise);
            Canvas.SetTop(_chooseExercise, 50);
            Canvas.SetLeft(_chooseExercise, GUIConstants.LEFT_OFFSET);

            int buttonWidth = (int)(display.Width * GUIConstants.BUTTON_WIDTH_PERCENTAGE);
            int buttonHeight = (int)(display.Height * GUIConstants.HEIGHT_PERCENTAGE);
            int bigButtonWidth = (int)(display.Width * GUIConstants.BIG_BUTTON_WIDTH_PERCENTAGE);
            int startY = 2 * buttonHeight;
            int startX = GUIConstants.LEFT_OFFSET;

            int minimum = System.Math.Min(MAX_EXERCISE_BUTTONS, exercises.Length);

            // Create all exercise buttons
            for (int i = 0; i < minimum; i++)
            {
                int xOffset = i % 2 == 0 ? 0 : buttonWidth + GUIConstants.DEFAULT_SPACING;
                int yOffset = i / 2 == 0 ? 0 : buttonHeight + GUIConstants.DEFAULT_SPACING;

                exButtons[i] = new ParameterizedRectangle(buttonWidth, buttonHeight);

                //add the given exercise as a single parameter to the ParameterizedRectangle with the default EXERCISE_KEY
                //so that whenever the TouchDown event is fired we can access the exercise associated with the button
                exButtons[i].AddParameter(EXERCISE_KEY, exercises[i]);
                
                exButtons[i].Fill = new SolidColorBrush(GUIConstants.NORMAL_BUTTON_COLOR);
                exButtons[i].SetMargin(GUIConstants.DEFAULT_MARGIN);
                buttonLabels[i] = new Text(GUIConstants.FONT, exercises[i].Name);
                buttonLabels[i].ForeColor = GUIConstants.TEXT_COLOR;

                Canvas.SetTop(exButtons[i], startY + yOffset);
                Canvas.SetLeft(exButtons[i], startX + xOffset);
                Canvas.SetTop(buttonLabels[i], startY + buttonHeight / 2 + yOffset);
                Canvas.SetLeft(buttonLabels[i], xOffset + startX + GUIConstants.DEFAULT_SPACING);

                canvas.Children.Add(exButtons[i]);
                canvas.Children.Add(buttonLabels[i]);

                //add the handler to TouchDown event of the Rectangle 
                exButtons[i].TouchDown += ExerciseSelectedHandler;
                buttonLabels[i].IsEnabled = false;
            }

            // Create a cancel button
            _cancelButton = new ParameterizedRectangle(bigButtonWidth, buttonHeight);
            _cancelButton.Fill = new SolidColorBrush(GUIConstants.SPECIAL_BUTTON_COLOR);

            _cancelButton.SetMargin(GUIConstants.DEFAULT_MARGIN);
            Canvas.SetTop(_cancelButton, startY + 2 * (buttonHeight + GUIConstants.DEFAULT_SPACING));
            Canvas.SetLeft(_cancelButton, startX);

            _cancelLabel = new Text(GUIConstants.FONT, CANCEL_TEXT);
            _cancelLabel.ForeColor = GUIConstants.TEXT_COLOR;
            Canvas.SetTop(_cancelLabel, startY + buttonHeight / 2 +
                2 * (buttonHeight + GUIConstants.DEFAULT_SPACING));
            Canvas.SetLeft(_cancelLabel, startX + GUIConstants.LOWER_BUTTON_LABEL_OFFSET);

            _cancelButton.TouchDown += OnCancelClicked;
            _cancelLabel.IsEnabled = false;
            canvas.Children.Add(_cancelButton);
            canvas.Children.Add(_cancelLabel);

            display.WPFWindow.Child = canvas;
        }
        /// <summary>
        /// When Cancel Clicked it switches the state to SelectEquipmentState
        /// </summary>
        /// <param name="sender">object fired the event</param>
        /// <param name="touchEventArgs">event args</param>
        private void OnCancelClicked(object sender, TouchEventArgs touchEventArgs)
        {
            stateManager.SwitchState(new SelectEquipmentState(display, stateManager));
        }

        /// <summary>
        /// Handles the click of a certain exercise.
        /// </summary>
        /// <param name="sender">object that triggered the event</param>
        /// <param name="touchEventArgs">other args.</param>
        private void ExerciseSelectedHandler(object sender, TouchEventArgs touchEventArgs)
        {
            ParameterizedRectangle r = (ParameterizedRectangle)sender;
            Exercise exercise = (Exercise)r.GetParameter(EXERCISE_KEY);
            // Set the chosen exercise in the shared session object
            stateManager.GetSession().Exercise = exercise;
            // Switch to the start exercise state
            stateManager.SwitchState(new StartExerciseState(display, stateManager));
        }

        public override void finish()
        {

        }
    }
}

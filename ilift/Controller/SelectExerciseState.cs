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
    class SelectExerciseState : ExecutionState
    {
        private const String SELECTED_EQUIPEMENT_TEXT = "Selected equipment: ";
        private const String CHOOSE_EXERCISE_TEXT = "Choose an exercise";
        private const String EXERCISE_KEY = "exercise";


        private Text _selectedEquipment;
        private Text _chooseExercise;

        private ParameterizedRectangle[] exButtons = new ParameterizedRectangle[4];
        private Text[] buttonLabels = new Text[4];
        
        public SelectExerciseState(DisplayTE35 display, StateManager state) : base(display, state)
        {

        }

        public override void init()
        {
            Exercise[] exercises = stateManager.GetSession().Equipment.Type.AvailableExercises;
            Font font = Resources.GetFont(Resources.FontResources.NinaB);

            _selectedEquipment = new Text(font, SELECTED_EQUIPEMENT_TEXT+stateManager.GetSession().Equipment.Type.Name);
            _selectedEquipment.ForeColor = Gadgeteer.Color.Green;
            canvas.Children.Add(_selectedEquipment);
            Canvas.SetTop(_selectedEquipment, 20);
            Canvas.SetLeft(_selectedEquipment, 10);

            _chooseExercise = new Text(font, CHOOSE_EXERCISE_TEXT);
            _chooseExercise.ForeColor = Gadgeteer.Color.Black;
            canvas.Children.Add(_chooseExercise);
            Canvas.SetTop(_chooseExercise, 50);
            Canvas.SetLeft(_chooseExercise, 10);

            int buttonWidth = (int) (display.Width*0.9);
            int buttonHeight = (int)(display.Height*0.15);
            int startY = 2*buttonHeight;
            int minimum = System.Math.Min(4, exercises.Length);
            for (int i = 0; i < minimum; i++)
            {
                exButtons[i] = new ParameterizedRectangle(buttonWidth, buttonHeight);
                exButtons[i].AddParameter(EXERCISE_KEY, exercises[i]);
                exButtons[i].Fill = new SolidColorBrush(Gadgeteer.Color.Gray);
                exButtons[i].SetMargin(10, 10, 10, 10);
                buttonLabels[i] = new Text(font,exercises[i].Name);
                buttonLabels[i].ForeColor = Gadgeteer.Color.Black;
                //exButtons[i].TouchDown += OnTouchDown;
                //Canvas.SetTop(buttonLabels[i],canvas.ActualHeight - );
                Canvas.SetTop(exButtons[i], startY + (i * (buttonHeight + 10)));
                Canvas.SetTop(buttonLabels[i], startY + buttonHeight / 2 + (i * (buttonHeight + 10)));
                Canvas.SetLeft(buttonLabels[i], 25);
                canvas.Children.Add(exButtons[i]);
                canvas.Children.Add(buttonLabels[i]);
                exButtons[i].TouchDown += ExerciseSelectedHandler;
            }
            display.WPFWindow.Child = canvas;
        }

        private void ExerciseSelectedHandler(object sender, TouchEventArgs touchEventArgs)
        {
            ParameterizedRectangle r = (ParameterizedRectangle)sender;
            Exercise exercise = (Exercise)r.GetParameter(EXERCISE_KEY);
            Debug.Print(exercise.Name);
            stateManager.GetSession().Exercise = exercise;
            stateManager.SwitchState(new StartExerciseState(display,stateManager));
        }

        public override void finish()
        {

        }
    }
}

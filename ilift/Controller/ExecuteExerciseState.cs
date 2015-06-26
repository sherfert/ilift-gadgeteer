using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;
using ilift.Patterns;
using Microsoft.SPOT.Presentation.Controls;

namespace ilift.Controller
{
    public class ExecuteExerciseState:ExecutionState
    {
        protected AbstractExercise exercise;
        protected DisplayTE35 display;
        protected StateManager stateManager;

        // For exercise state
        protected Text _repNumber;
        Font font; 

        public ExecuteExerciseState(DisplayTE35 display, StateManager state, AbstractExercise exercise)
            : base(display, state)
        {

            this.exercise = exercise;
        }

        // Put all Gui drawing init codes here
        public override void init()
        {
            display.WPFWindow.UpdateLayout(); //TODO do we need this?
            font = Resources.GetFont(Resources.FontResources.NinaB);
            exercise.onRepetitionDone += UpdateScreen;
            exercise.StartExercise();
            _repNumber = new Text(font, "0");
            Canvas.SetTop(_repNumber, display.Height / 3);
            Canvas.SetLeft(_repNumber, display.Width / 2);

            canvas.Children.Add(_repNumber);
        }

        public void UpdateScreen()
        {

        }

        public override void finish()
        {
            exercise.StopExercise();
        }
    }
}

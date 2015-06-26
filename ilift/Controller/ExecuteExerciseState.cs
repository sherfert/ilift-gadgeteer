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
 

        // For exercise state
        protected Text _repNumber;
        protected int count;

        public ExecuteExerciseState(DisplayTE35 display, StateManager state, AbstractExercise exercise)
            : base(display, state)
        {
            this.exercise = exercise;
        }

        // Put all Gui drawing init codes here
        public override void init()
        {
            count = 0;
            display.WPFWindow.UpdateLayout(); //TODO do we need this?
            Font font = Resources.GetFont(Resources.FontResources.NinaB);
            exercise.onRepetitionDone += UpdateScreen;
            exercise.StartExercise();
            _repNumber = new Text(font, "0");
            Canvas.SetTop(_repNumber, display.Height / 3);
            Canvas.SetLeft(_repNumber, display.Width / 2);

            canvas.Children.Add(_repNumber);
            display.WPFWindow.Child = canvas;
        }

        public void UpdateScreen()
        {
            count++;
            _repNumber.TextContent = count.ToString();
        }

        public override void finish()
        {
            exercise.StopExercise();
        }
    }
}

using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;
using Microsoft.SPOT.Presentation.Controls;
using ilift.Model;
using System.Collections;
using ilift.Patterns;

namespace ilift.Controller
{
    public class StartExerciseState:ExecutionState
    {
        
        private const string POSITION_TEXT = "Go to start position";
        private const int TIMER_INIT = 3;


        private Text _execTitleLabel;
        private Text _positionLabel;
        private Text _timerLabel;

        private Gadgeteer.Timer _tickTimer = new Gadgeteer.Timer(1000);
        private int _currentTimerValue = TIMER_INIT;
        private AbstractExercise exercise;

        
        public StartExerciseState(DisplayTE35 display, StateManager state) : base(display, state)
        {
            
            // FIXME remove mockup data
            //Hashtable table = new Hashtable();
            //table["id"] = 1L;
            //table["name"] = "Biceps curl";
            //Exercise e = new Exercise(table);
            //stateManager.GetSession().Exercise = e;
            String exerciseID = stateManager.GetSession().Exercise.Id.ToString();
            Debug.Print(exerciseID);
            exercise = ExerciseManager.GetExercise(stateManager.GetSession().Exercise.Id);
        }
        public override void init()
        {
            display.WPFWindow.UpdateLayout(); //TODO do we need this?
            Font font = Resources.GetFont(Resources.FontResources.NinaB);
            
            _execTitleLabel = new Text(font, stateManager.GetSession().Exercise.Name);
            
            Canvas.SetTop(_execTitleLabel, 50);
            Canvas.SetLeft(_execTitleLabel, 100);

            _positionLabel = new Text(font, POSITION_TEXT);

            Canvas.SetTop(_positionLabel, 100);
            Canvas.SetLeft(_positionLabel, 100);
            // TODO make the timer bigger
            _timerLabel = new Text(font, "");
            _timerLabel.ForeColor = Gadgeteer.Color.Red;
            

            Canvas.SetTop(_timerLabel, 150);
            Canvas.SetLeft(_timerLabel, 150);

            canvas.Children.Add(_execTitleLabel);
            canvas.Children.Add(_positionLabel);
            canvas.Children.Add(_timerLabel);

            display.WPFWindow.Child = canvas;


            // Start the timer
            _tickTimer.Tick += Timer;
            _tickTimer.Start();
            
        }

        private void Timer(Gadgeteer.Timer timer)
        {
            if (_currentTimerValue == 0)
            {
                _tickTimer.Stop();
                //_tickTimer.Tick -= Timer;
                Calibrate();
            }
            else
            {
                _timerLabel.TextContent = _currentTimerValue.ToString();
                _currentTimerValue--;
            }
        }

        private void Calibrate()
        {
            Canvas.SetLeft(_timerLabel, 100);
            _timerLabel.TextContent = "Calibrating...";
            exercise.Calibrate(stateManager.GetHardwareController().GetAccelerometer());

            // TODO switch to next screen
            stateManager.SwitchState(new ExecuteExerciseState(display, stateManager, exercise));
        }

        public override void finish()
        {
            
        }
    }
}

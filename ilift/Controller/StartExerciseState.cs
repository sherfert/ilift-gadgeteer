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

        /// <summary>
        /// This is a state between ExecuteExerciseState
        /// </summary>
        /// <param name="display">display hardware</param>
        /// <param name="state">state manager</param>
        public StartExerciseState(DisplayTE35 display, StateManager state) : base(display, state)
        {
            String exerciseID = stateManager.GetSession().Exercise.Id.ToString();
            Debug.Print(exerciseID);
            exercise = ExerciseManager.GetExercise(stateManager.GetSession().Exercise.Id);
        }

        public override void init()
        {
            Font font = Resources.GetFont(Resources.FontResources.NinaB);
            Font counterFont = Resources.GetFont(Resources.FontResources.counter);
            _execTitleLabel = new Text(font, stateManager.GetSession().Exercise.Name);
            
            Canvas.SetTop(_execTitleLabel, 50);
            Canvas.SetLeft(_execTitleLabel, 120);

            _positionLabel = new Text(font, POSITION_TEXT);

            Canvas.SetTop(_positionLabel, 100);
            Canvas.SetLeft(_positionLabel, 100);
            // TODO make the timer bigger
            _timerLabel = new Text(counterFont, "");
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

        /// <summary>
        /// Handler in ticks generated by gadgeteer 
        /// </summary>
        /// <param name="timer">the timer hardware</param>
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

        /// <summary>
        /// Calibrates the sensor and switches to the next screen
        /// </summary>
        private void Calibrate()
        {
            Canvas.SetLeft(_timerLabel, 100);
            _timerLabel.TextContent = "Calibrating...";

            exercise.Initialize(stateManager.GetHardwareController().GetAccelerometer(), stateManager.GetHardwareController().GetTunes());


            stateManager.SwitchState(new ExecuteExerciseState(display, stateManager, exercise));
        }

        public override void finish()
        {
            
        }
    }
}

using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;

namespace ilift.Patterns
{
    public delegate void RepetitionDelegate(Boolean hasGoodQuality);
    public delegate void SubPatterDoneDelegate(String msg);

    /// <summary>
    /// Abstract exercise class that implements all the common methods for all exercises
    /// </summary>
    public abstract class AbstractExercise
    {
        /// <summary>
        /// Delegate for when repetition is done
        /// </summary>
        public event RepetitionDelegate onRepetitionDone;
        /// <summary>
        /// Delegate for when sub pattern is recognised
        /// </summary>
        public event SubPatterDoneDelegate onSubPatternDone;

        protected Accelerometer accelerometer;
        protected Tunes tunes;
        protected Boolean isGoodQuality;

        protected AbstractExercise()
        {
        }

        /// <summary>
        /// Called from concrete exercise class to pass accelerometer data to the complex pattern
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public abstract void ProcessData(double x, double y, double z);

        /// <summary>
        /// Calibrates the accelerometer for the start position and also takes as argument tunes
        /// Initializes also the quality of the repetition as good quality
        /// </summary>
        /// <param name="accelerometer"></param>
        /// <param name="tunes"></param>
        public void Initialize(Accelerometer accelerometer, Tunes tunes)
        {
            this.tunes = tunes;
            this.accelerometer = accelerometer;
            accelerometer.Calibrate();
            this.isGoodQuality = true;
           
        }

        /// <summary>
        /// Starts the exercise by calling attaching handler to measurment complete.
        /// Starts taking measurments from the accelerometer.
        /// Play a tune to indicate exercise starts.
        /// </summary>
        public void StartExercise()
        {
            accelerometer.MeasurementComplete += AcceptMeasurement;
            accelerometer.StartTakingMeasurements();
            tunes.Play(new Tunes.MusicNote(new Tunes.Tone(666), 250));
        }

        /// <summary>
        /// Handler called by the accelerometer on measurment complete.
        /// Passes the args from accelerometer to ProcessData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void AcceptMeasurement(Accelerometer sender, Accelerometer.MeasurementCompleteEventArgs args)
        {
            ProcessData(args.X, args.Y, args.Z);
        }

        /// <summary>
        /// Called when exercise is to end.
        /// Stops taking measurments from accelerometer.
        /// Unregister the handler from measurment complete
        /// </summary>
        public void StopExercise()
        {
            accelerometer.StopTakingMeasurements();
            accelerometer.MeasurementComplete -= AcceptMeasurement;
        }

        /// <summary>
        /// Handler for repetition done within exercise.
        /// Takes as arquement the quality of the exercise and the message passed for display. 
        /// Message related to the quality of the exercise.
        /// If is good quality plays positive tune.
        /// </summary>
        /// <param name="quality"></param>
        /// <param name="msg"></param>
        public void onRepetitionDoneHandler(Quality quality, String msg)
        {
            
            if(isGoodQuality)
                tunes.Play(new Tunes.MusicNote(new Tunes.Tone(666), 250));

            onRepetitionDone(isGoodQuality);
            isGoodQuality = true;
           
        }
        /// <summary>
        /// Handler for sub pattern  done within exercise.
        /// Takes as arquement the quality of the exercise and the message passed for display. 
        /// Message related to the quality of the exercise.
        /// If sub pattern is bad quality plays negative tune.
        /// </summary>
        /// <param name="subPatternQuality"></param>
        /// <param name="msg"></param>
        public void onSubPatternDoneHandler(Quality subPatternQuality, String msg)
        {
            switch (subPatternQuality) {
                case Quality.BAD:
                     tunes.Play(new Tunes.MusicNote(new Tunes.Tone(321), 250));
                     isGoodQuality = false;
                     break;
                default:
                     break;
            }

            onSubPatternDone(msg);
           
        }

    }
}

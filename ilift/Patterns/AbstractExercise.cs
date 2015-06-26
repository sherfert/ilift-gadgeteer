using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;

namespace ilift.Patterns
{
    public delegate void RepetitionDelegate();
    // TODO comment this shit
    public abstract class AbstractExercise
    {
        public event RepetitionDelegate onRepetitionDone;

        protected Accelerometer accelerometer;

        protected AbstractExercise()
        {
            
        }

        public abstract void ProcessData(double x, double y, double z);

        public void Calibrate(Accelerometer accelerometer)
        {
            this.accelerometer = accelerometer;
            accelerometer.Calibrate();
           
        }

        public void StartExercise()
        {
            accelerometer.MeasurementComplete += AcceptMeasurement;
            accelerometer.StartTakingMeasurements();
        }

        private void AcceptMeasurement(Accelerometer sender, Accelerometer.MeasurementCompleteEventArgs args)
        {
            ProcessData(args.X, args.Y, args.Z);
        }

        public void StopExercise()
        {
            accelerometer.StopTakingMeasurements();
            accelerometer.MeasurementComplete -= AcceptMeasurement;
        }

        public void onRepetitionDoneHandler()
        {
            onRepetitionDone();
        }

    }
}

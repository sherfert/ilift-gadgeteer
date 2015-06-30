using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;

namespace ilift.Patterns
{
    public delegate void RepetitionDelegate(Boolean hasGoodQuality);
    public delegate void SubPatterDoneDelegate(String msg);
    // TODO comment this shit
    public abstract class AbstractExercise
    {
        public event RepetitionDelegate onRepetitionDone;
        public event SubPatterDoneDelegate onSubPatternDone;

        protected Accelerometer accelerometer;
        protected Tunes tunes;
        protected Boolean isGoodQuality;

        protected AbstractExercise()
        {
        }

        public abstract void ProcessData(double x, double y, double z);

        public void Initialize(Accelerometer accelerometer, Tunes tunes)
        {
            this.tunes = tunes;
            this.accelerometer = accelerometer;
            accelerometer.Calibrate();
            this.isGoodQuality = true;
           
        }

        public void StartExercise()
        {
            accelerometer.MeasurementComplete += AcceptMeasurement;
            accelerometer.StartTakingMeasurements();
            tunes.Play(new Tunes.MusicNote(new Tunes.Tone(666), 250));
        }

        private void AcceptMeasurement(Accelerometer sender, Accelerometer.MeasurementCompleteEventArgs args)
        {
            //onMeasurmentsTaken(args.X, args.Y, args.Z);

            ProcessData(args.X, args.Y, args.Z);
        }

        public void StopExercise()
        {
            accelerometer.StopTakingMeasurements();
            accelerometer.MeasurementComplete -= AcceptMeasurement;
        }

        public void onRepetitionDoneHandler(Quality quality, String msg)
        {
            
            if(isGoodQuality)
                tunes.Play(new Tunes.MusicNote(new Tunes.Tone(666), 250));

            onRepetitionDone(isGoodQuality);
            isGoodQuality = true;
           
        }

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

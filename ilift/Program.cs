using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

using ilift.Patterns;
using ilift.Controller;

namespace ilift
{
    public partial class Program : HardwareController
    {
        BicepCurl bicepCurl;
        LateralRaise lateralRaise;
        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            AppController gameController = new AppController(this);
            
            return;
            // FIXME unreachable Ilmi's code

            button.ButtonPressed += new GTM.GHIElectronics.Button.ButtonEventHandler(button_ButtonPressed);
            accelerometer.MeasurementInterval = new TimeSpan(0, 0, 0, 0, 100);
            accelerometer.MeasurementComplete += new 
                GTM.GHIElectronics.Accelerometer.MeasurementCompleteEventHandler(accelerometer_MeasurementComplete);
            compass.MeasurementInterval = new TimeSpan(0, 0, 0, 1);
            compass.MeasurementComplete += new 
                GTM.GHIElectronics.Compass.MeasurementCompleteEventHandler(compass_MeasurementComplete);

            //bicepCurlPattern = new BicepCurlPattern();
            bicepCurl = new BicepCurl();
            bicepCurl.onRepetitionDone += bicepCurl_onRepetitionDone;

            lateralRaise = new LateralRaise();
            lateralRaise.onRepetitionDone += bicepCurl_onRepetitionDone;
            
            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started");
        }

        void bicepCurl_onRepetitionDone()
        {
            Debug.Print("Repetition Done!");
        }

        void compass_MeasurementComplete(GTM.GHIElectronics.Compass sender, 
            GTM.GHIElectronics.Compass.MeasurementCompleteEventArgs e)
        {
            //Debug.Print("Compass:\tx: " + e.X + "\ty: " + e.Y + "\tz: " + e.Z + "\n");
        }

        void button_ButtonPressed(GTM.GHIElectronics.Button sender, GTM.GHIElectronics.Button.ButtonState state)
        {
            //compass.StartTakingMeasurements();

            accelerometer.Calibrate();
            accelerometer.StartTakingMeasurements();
            Debug.Print("Calibrated");
        }

        void accelerometer_MeasurementComplete(
            GTM.GHIElectronics.Accelerometer sender, 
            GTM.GHIElectronics.Accelerometer.MeasurementCompleteEventArgs e)
        {
            //bicepCurl.processData(e.X, e.Y, e.Z);
            lateralRaise.processData(e.X, e.Y, e.Z);
        }

        public void RegisterButtonPressedHandler(Button.ButtonEventHandler handler)
        {
            button.ButtonPressed += handler;
        }

        public void RegisterDisplayTouchedHandler(GUITouchDelegate handler)
        {
            // TODO
            //throw new NotImplementedException();
        }

        public void RegisterRFIDReadHandler(RFIDReadDelegate handler)
        {
            // TODO
            //throw new NotImplementedException();
        }

        public Accelerometer GetAccelerometer()
        {
            return accelerometer;
        }

        public Compass GetCompass()
        {
            return compass;
        }

        public DisplayTE35 GetDisplay()
        {
            return displayTE35;
        }
    }
}

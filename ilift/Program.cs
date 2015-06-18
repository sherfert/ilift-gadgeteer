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

namespace ilift
{
    public partial class Program
    {
        ComplexPattern bicepCurlPattern;
        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            /*******************************************************************************************
            Modules added in the Program.gadgeteer designer view are used by typing 
            their name followed by a period, e.g.  button.  or  camera.
            
            Many modules generate useful events. Type +=<tab><tab> to add a handler to an event, e.g.:
                button.ButtonPressed +=<tab><tab>
            
            If you want to do something periodically, use a GT.Timer and handle its Tick event, e.g.:
                GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
                timer.Tick +=<tab><tab>
                timer.Start();
            *******************************************************************************************/
            button.ButtonPressed += new GTM.GHIElectronics.Button.ButtonEventHandler(button_ButtonPressed);
            accelerometer.MeasurementInterval = new TimeSpan(0, 0, 0, 0, 100);
            accelerometer.MeasurementComplete += new 
                GTM.GHIElectronics.Accelerometer.MeasurementCompleteEventHandler(accelerometer_MeasurementComplete);
            compass.MeasurementInterval = new TimeSpan(0, 0, 0, 1);
            compass.MeasurementComplete += new 
                GTM.GHIElectronics.Compass.MeasurementCompleteEventHandler(compass_MeasurementComplete);

            //bicepCurlPattern = new BicepCurlPattern();
            bicepCurlPattern = new ComplexPattern();
            bicepCurlPattern.addPattern(new CurlDownPattern());
            bicepCurlPattern.addPattern(new CurlUpPattern());
            bicepCurlPattern.onActionDone += bicepCurlPattern_onActionDone;
            
            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started");
        }

        void bicepCurlPattern_onActionDone()
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
            bicepCurlPattern.processAccelData(e.X, e.Y, e.Z);
        }
    }
}

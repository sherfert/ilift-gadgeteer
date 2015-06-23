using System;
using System.Collections;
using System.Threading;
using GHI.Networking;
using ilift.Model;
using ilift.Network;
using Json.NETMF;
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
            Debug.Print("Program started");
            AccelerometerInit();
            AppController appController = new AppController(this);

            wifiRS21.NetworkInterface.Open();
            ArrayList list = new ArrayList();
            WiFiRS9110.NetworkParameters[] results = wifiRS21.NetworkInterface.Scan();
            //TODO extract this code to another place 
            foreach (var netInterface in results)
            {
                if (netInterface.Ssid.Equals("AndroidAP"))
                {
                    netInterface.Key = "tk3-umundo";
                    wifiRS21.NetworkInterface.Join(netInterface);
                    Debug.Print("NetworkCOnnected:" + wifiRS21.IsNetworkConnected);
                    wifiRS21.NetworkUp += WifiRs21OnNetworkUp;
                    break;
                }
                Debug.Print(netInterface.Ssid);
            }                 
            
            // FIXME unreachable Ilmi's code

            //button.ButtonPressed += new GTM.GHIElectronics.Button.ButtonEventHandler(button_ButtonPressed);
            //accelerometer.MeasurementInterval = new TimeSpan(0, 0, 0, 0, 100);
            //accelerometer.MeasurementComplete += new 
            //    GTM.GHIElectronics.Accelerometer.MeasurementCompleteEventHandler(accelerometer_MeasurementComplete);
            //compass.MeasurementInterval = new TimeSpan(0, 0, 0, 1);
            //compass.MeasurementComplete += new 
            //    GTM.GHIElectronics.Compass.MeasurementCompleteEventHandler(compass_MeasurementComplete);

            ////bicepCurlPattern = new BicepCurlPattern();
            //bicepCurl = new BicepCurl();
            //bicepCurl.onRepetitionDone += bicepCurl_onRepetitionDone;

            //lateralRaise = new LateralRaise();
            //lateralRaise.onRepetitionDone += bicepCurl_onRepetitionDone;
            
            //button.ButtonPressed += delegate(Button sender, Button.ButtonState state)
            //{
                
            //    NetworkClient.GetEquipmentByTag("4D0055BA45",
            //        equipment =>
            //        {
            //            NetworkClient.GetUser("satiaherfert", user =>
            //            {
            //                Session session = new Session(user, equipment.Type.AvailableExercises[0], 400, equipment);
            //                NetworkClient.PostSession(session);
            //                Debug.Print("We send a post request with repetitions " + session.Repetitions);
            //            });
            //        });
            //};
            //accelerometer.MeasurementInterval = new TimeSpan(0,0,0,200);
            //accelerometer.MeasurementComplete +=
            //    delegate(Accelerometer sender, Accelerometer.MeasurementCompleteEventArgs args)
            //    {
            //        Debug.Print("X value " + args.X);
            //        Debug.Print("Y value " + args.Y);
            //        Debug.Print("Z value " + args.Z);
            //    };
            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            //Debug.Print("Program Started");
            //this.rfidReader.IdReceived += this.rfidReader_IdReceived;
            //this.rfidReader.MalformedIdReceived += this.rfidReader_MalformedIdReceived;
            //Debug.Print(wifiRS21.NetworkSettings.IPAddress);
			// TODO extract this to a method
            

        }

        private void AccelerometerInit() {
            accelerometer.MeasurementInterval = new TimeSpan(0, 0, 0, 0, 100);
        }

        private void WifiRs21OnNetworkUp(GTM.Module.NetworkModule sender, GTM.Module.NetworkModule.NetworkState state)
        {
            
            Debug.Print("Now we are Network UP");
           
        }

    

        private void rfidReader_IdReceived(RFIDReader sender, string e)
        {
            Debug.Print("RFID scanned: " + e);
            NetworkClient.GetUser(e,user => 
                    Debug.Print("This user is : " + user.username)
                );
        }

        private void rfidReader_MalformedIdReceived(RFIDReader sender, EventArgs e)
        {
            Debug.Print("Please rescan your card");
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
            lateralRaise.ProcessData(e.X, e.Y, e.Z);
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

        public void RegisterRFIDReadHandler(RFIDReader.IdReceivedEventHandler handler)
        {
            this.rfidReader.IdReceived += handler;
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

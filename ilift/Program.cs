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
           
            NetworkClient.ConnectToWifi(wifiRS21,OnNetworkUp);
        }

        private void AccelerometerInit() {
            accelerometer.MeasurementInterval = new TimeSpan(0, 0, 0, 0, 100);
        }

        private void OnNetworkUp()
        {
            Debug.Print("Now we are Network UP");
            AppController appController = new AppController(this);

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

        //void button_ButtonPressed(GTM.GHIElectronics.Button sender, GTM.GHIElectronics.Button.ButtonState state)
        //{
        //    //compass.StartTakingMeasurements();

        //    accelerometer.Calibrate();
        //    accelerometer.StartTakingMeasurements();
        //    Debug.Print("Calibrated");
        //}

        void accelerometer_MeasurementComplete(
            GTM.GHIElectronics.Accelerometer sender, 
            GTM.GHIElectronics.Accelerometer.MeasurementCompleteEventArgs e)
        {
            //bicepCurl.processData(e.X, e.Y, e.Z);
            lateralRaise.ProcessData(e.X, e.Y, e.Z);
        }

        //public void RegisterButtonPressedHandler(Button.ButtonEventHandler handler)
        //{
        //    button.ButtonPressed += handler;
        //}

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

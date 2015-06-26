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
    /// <summary>
    /// Program entry point.
    /// </summary>
    public partial class Program : HardwareController
    {
        /// <summary>
        /// This method is run when the mainboard is powered up or reset.
        /// </summary>
        void ProgramStarted()
        {
            Debug.Print("Program started");
            AccelerometerInit();
            NetworkClient.ConnectToWifi(wifiRS21,OnNetworkUp);
        }

        /// <summary>
        /// Initialize the accelerometer.
        /// </summary>
        private void AccelerometerInit() {
            accelerometer.MeasurementInterval = new TimeSpan(0, 0, 0, 0, 100);
        }

        /// <summary>
        /// Start the app controller once the network is up.
        /// </summary>
        private void OnNetworkUp()
        {
            Debug.Print("Now we are Network UP");
            AppController appController = new AppController(this);

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

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

namespace ilift
{
    public partial class Program
    {
        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            
            button.ButtonPressed += delegate(Button sender, Button.ButtonState state)
            {
                
                NetworkClient.GetEquipmentByTag("4D0055BA45",
                    equipment =>
                    {
                        NetworkClient.GetUser("satiaherfert", user =>
                        {
                            Session session = new Session(user, equipment.Type.AvailableExercises[0], 400, equipment);
                            NetworkClient.PostSession(session);
                            Debug.Print("We send a post request with repetitions " + session.Repetitions);
                        });
                    });
            };
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
            //accelerometer.MeasurementInterval = new TimeSpan(0,0,0,200);
            //accelerometer.MeasurementComplete +=
            //    delegate(Accelerometer sender, Accelerometer.MeasurementCompleteEventArgs args)
            //    {
            //        Debug.Print("X value " + args.X);
            //        Debug.Print("Y value " + args.Y);
            //        Debug.Print("Z value " + args.Z);
            //    };
            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started");
            this.rfidReader.IdReceived += this.rfidReader_IdReceived;
            this.rfidReader.MalformedIdReceived += this.rfidReader_MalformedIdReceived;
            //Debug.Print(wifiRS21.NetworkSettings.IPAddress);
            wifiRS21.NetworkInterface.Open();
            ArrayList list = new ArrayList();
            WiFiRS9110.NetworkParameters[] results = wifiRS21.NetworkInterface.Scan();

            foreach (var netInterface in results)
            {
                if (netInterface.Ssid.Equals("AndroidAP"))
                {
                    netInterface.Key = "tk3-umundo";
                    wifiRS21.NetworkInterface.Join(netInterface);
                    Debug.Print("NetworkCOnnected:" + wifiRS21.IsNetworkConnected);
                    wifiRS21.NetworkUp += WifiRs21OnNetworkUp;
                }
                Debug.Print(netInterface.Ssid);
            }

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
    }
}

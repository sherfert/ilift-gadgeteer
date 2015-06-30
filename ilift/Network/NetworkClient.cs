using System;
using System.Collections;
using Gadgeteer.Modules.GHIElectronics;
using Gadgeteer.Networking;
using GHI.Networking;
using ilift.Model;
using Json.NETMF;
using Microsoft.SPOT;

namespace ilift.Network
{
    public delegate void UserReply(User user);
    public delegate void EquipmentReply(Equipment equipment);
    public delegate void NetworkConnected();
  
    /// <summary>
    /// NetworkClient that connects to WiFi and offers possibilities to communicate with the REST service
    /// </summary>
    public class NetworkClient
    {
        //the address of the REST service
        public const string ADDRESS = "http://192.168.43.181:8080/ilift/";
        //login credentials wifi
        private const string SSID = "AndroidAP";
        private const string KEY = "tk3-umundo";

        /// <summary>
        /// Opens hardware network interface and tries to connect with the SSID and KEY
        /// </summary>
        /// <param name="wifi">The wifi hardware</param>
        /// <param name="networkConnectedDelegate">Delegate after the wifi is set up and network is up and running</param>
        public static void ConnectToWifi(WiFiRS21 wifi,NetworkConnected networkConnectedDelegate)
        {
            wifi.NetworkInterface.Open();
            ArrayList list = new ArrayList();
            WiFiRS9110.NetworkParameters[] results = wifi.NetworkInterface.Scan();
            foreach (var netInterface in results)
            {
                if (netInterface.Ssid.Equals(SSID))
                {
                    netInterface.Key = KEY;
                    wifi.NetworkInterface.Join(netInterface);
                    Debug.Print("NetworkConnected:" + wifi.IsNetworkConnected);
                    wifi.NetworkUp += (sender, state) => networkConnectedDelegate();
                    break;
                }
                Debug.Print(netInterface.Ssid);
            }                 
            
        }

        /// <summary>
        /// Get the user with specified rfid.
        /// </summary>
        /// <param name="rfid">Rfid scanned</param>
        /// <param name="userReplyDelegate">Delegate the execution after the response</param>
        public static void GetUser(string rfid, UserReply userReplyDelegate)
        {
            HttpRequest wc = WebClient.GetFromWeb(ADDRESS + 
                "user/" + rfid);
            wc.ResponseReceived += (sender, response) => 
            {
                Debug.Print(response.Text);
                if (response.Text == "null")
                {
                    userReplyDelegate(null);
                }
                else
                {
                    Hashtable userHt = JsonSerializer.DeserializeString(response.Text) as Hashtable;
                    userReplyDelegate(new User(userHt));
                }
            };
        }

        /// <summary>
        /// Get the equipment with the scanned rfid tag 
        /// </summary>
        /// <param name="tag">Rfid tag</param>
        /// <param name="equipmentReplyDelegate">Delegate the execution after the response</param>
        public static void GetEquipmentByTag(string tag, EquipmentReply equipmentReplyDelegate)
        {
            HttpRequest wc = WebClient.GetFromWeb(ADDRESS +
                "equipment/" + tag);
            wc.ResponseReceived += (sender, response) =>
            {
                Debug.Print(response.Text);
                if (response.Text == "null")
                {
                    equipmentReplyDelegate(null);
                }
                else
                {
                    Hashtable equipmentHt = JsonSerializer.DeserializeString(response.Text) as Hashtable;
                    equipmentReplyDelegate(new Equipment(equipmentHt));
                }
            };
        }

        /// <summary>
        /// Issue a post request to insert the given session
        /// </summary>
        /// <param name="session">The given session</param>
        public static void PostSession(Session session)
        {
            POSTContent pc =
                POSTContent.CreateTextBasedContent(JsonSerializer.SerializeObject(session.ConstructHashtable()));
            HttpRequest rq = HttpHelper.CreateHttpPostRequest(ADDRESS + "session/",pc, "application/json");
            rq.SendRequest();
        }
    }
}

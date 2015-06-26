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
  

    public class NetworkClient
    {
        public const string ADDRESS = "http://192.168.43.181:8080/ilift/";
        private const string SSID = "AndroidAP";
        private const string KEY = "tk3-umundo";


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

        public static void PostSession(Session session)
        {
            POSTContent pc =
                POSTContent.CreateTextBasedContent(JsonSerializer.SerializeObject(session.ConstructHashtable()));
            HttpRequest rq = HttpHelper.CreateHttpPostRequest(ADDRESS + "session/",pc, "application/json");
            rq.SendRequest();
        }
    }
}

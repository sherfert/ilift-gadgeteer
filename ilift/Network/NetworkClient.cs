using System;
using System.Collections;
using Gadgeteer.Networking;
using ilift.Model;
using Json.NETMF;
using Microsoft.SPOT;

namespace ilift.Network
{
    public delegate void UserReply(User user);
    public delegate void EquipmentReply(Equipment equipment);

    public class NetworkClient
    {
        public const string ADDRESS = "http://192.168.43.191:8080/ilift/";

        public static void GetUser(string rfid, UserReply userReplyDelegate)
        {
            HttpRequest wc = WebClient.GetFromWeb(ADDRESS + 
                "user/" + rfid);
            wc.ResponseReceived += (sender, response) => 
            {
                Debug.Print(response.Text);
                Hashtable userHt = JsonSerializer.DeserializeString(response.Text) as Hashtable;
                userReplyDelegate(new User(userHt));
            };
        }

        public static void GetEquipmentByTag(string tag, EquipmentReply equipmentReplyDelegate)
        {
            HttpRequest wc = WebClient.GetFromWeb(ADDRESS +
                "equipment/" + tag);
            wc.ResponseReceived += (sender, response) =>
            {
                Debug.Print(response.Text);
                Hashtable equipmentHt = JsonSerializer.DeserializeString(response.Text) as Hashtable;
                equipmentReplyDelegate(new Equipment(equipmentHt));
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

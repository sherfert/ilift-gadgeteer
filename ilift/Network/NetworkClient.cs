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
        public const string ADDRESS = "http://192.168.43.181:8080/ilift/";

        public static void GetUser(string username, UserReply userReplyDelegate)
        {
            HttpRequest wc = WebClient.GetFromWeb(ADDRESS + 
                "user/byUsername/" + username);
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


    }
}

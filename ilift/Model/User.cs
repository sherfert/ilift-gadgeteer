using System;
using System.Collections;
using Microsoft.SPOT;

namespace ilift.Model
{
    public class User
    {
        public long id;
        public String rfidTag;
        public String username;

        public User(Hashtable hashtable)
        {
            id = (long) hashtable["id"];
            rfidTag = (string) hashtable["rfidTag"];
            username = (string)hashtable["username"];
            // sessions not needed, so we don't care
        }

        
    }
}

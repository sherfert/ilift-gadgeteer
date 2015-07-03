using System;
using System.Collections;
using Microsoft.SPOT;

namespace ilift.Model
{
    /// <summary>
    /// Represents a user
    /// </summary>
    public class User
    {
        public long id;
        public String rfidTag;
        public String username;

        /// <summary>
        /// Constructs a user out of hashtable 
        /// </summary>
        /// <param name="hashtable"></param>
        public User(Hashtable hashtable)
        {
            id = (long) hashtable["id"];
            rfidTag = (string) hashtable["rfidTag"];
            username = (string)hashtable["username"];
            // sessions not needed, so we don't care
        }


        /// <summary>
        /// Hashtable representation of this user
        /// </summary>
        /// <returns></returns>
        internal object ConstructHashtable()
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("id", id);
            return hashtable;
        }
    }
}

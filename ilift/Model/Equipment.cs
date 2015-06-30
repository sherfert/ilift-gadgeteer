using System;
using System.Collections;
using Microsoft.SPOT;

namespace ilift.Model
{
    /// <summary>
    /// Represent the Equipment 
    /// </summary>
    public class Equipment
    {
        private long id;
        private string rfidTag;
        private double weightKg;
        private EqType type;

        /// <summary>
        /// Construct an Equipment out of an Hashtable 
        /// </summary>
        /// <param name="hashtable"></param>
        public Equipment(Hashtable hashtable)
        {
            id = (long) hashtable["id"];
            rfidTag = (string) hashtable["rfidTag"];
            weightKg = (double) hashtable["weightKg"];
            type = new EqType((Hashtable) hashtable["type"]);
        }

        /// <summary>
        /// Equipment Type
        /// </summary>
        public EqType Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// The Weight of the equipment in KG
        /// </summary>
        public double WeightKg
        {
            get { return weightKg; }
            set { weightKg = value; }
        }

        /// <summary>
        /// Rfid attached to the equipment
        /// </summary>
        public string RfidTag
        {
            get { return rfidTag; }
            set { rfidTag = value; }
        }

        /// <summary>
        /// Id that defines uniquely 
        /// </summary>
        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Hashtable representation of this object
        /// </summary>
        /// <returns>Hashtable objec</returns>
        internal object ConstructHashTable()
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("id", id);
            return hashtable;
        }
    }
}

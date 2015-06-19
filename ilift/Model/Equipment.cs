using System;
using System.Collections;
using Microsoft.SPOT;

namespace ilift.Model
{
    public class Equipment
    {
        private long id;
        private string rfidTag;
        private double weightKg;
        private EqType type;

        public Equipment(Hashtable hashtable)
        {
            id = (long) hashtable["id"];
            rfidTag = (string) hashtable["rfidTag"];
            weightKg = (double) hashtable["weightKg"];
            type = new EqType((Hashtable) hashtable["type"]);
        }

        public EqType Type
        {
            get { return type; }
            set { type = value; }
        }

        public double WeightKg
        {
            get { return weightKg; }
            set { weightKg = value; }
        }

        public string RfidTag
        {
            get { return rfidTag; }
            set { rfidTag = value; }
        }

        public long Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}

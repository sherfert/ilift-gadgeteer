using System;
using System.Collections;
using Microsoft.SPOT;

namespace ilift.Model
{
    public class Session
    {
        private long id;
        private User user;
        private Exercise exercise;
        private long repetitions;
        private Equipment equipment;

        public Session()
        {
            
        }

        public Session(User user, Exercise exercise, long repetitions, Equipment eq)
        {
            User = user;
            Exercise = exercise;
            Repetitions = repetitions;
            Equipment = eq;
        }

        public Hashtable ConstructHashtable()
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("id", id);
            hashtable.Add("user", user.ConstructHashtable());
            hashtable.Add("exercise", exercise.ConstructHashtable());
            hashtable.Add("equipment", equipment.ConstructHashTable());
            hashtable.Add("repetitions", repetitions);
            return hashtable;
        }


        public Equipment Equipment
        {
            get { return equipment; }
            set { equipment = value; }
        }

        public long Repetitions
        {
            get { return repetitions; }
            set { repetitions = value; }
        }

        public Exercise Exercise
        {
            get { return exercise; }
            set { exercise = value; }
        }

        public User User
        {
            get { return user; }
            set { user = value; }
        }

        public long Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}

using System;
using System.Collections;
using Microsoft.SPOT;

namespace ilift.Model
{
    /// <summary>
    /// Represent the Session model
    /// </summary>
    public class Session
    {
        private long id;
        private User user;
        private Exercise exercise;
        private long repetitions;
        private Equipment equipment;
        
        /// <summary>
        /// Empty constructor 
        /// </summary>
        public Session()
        {
            
        }

        /// <summary>
        /// Concstructs a new Session
        /// </summary>
        /// <param name="user">User associated to this session</param>
        /// <param name="exercise">Exercise associated to this session</param>
        /// <param name="repetitions">Number of repetitions done</param>
        /// <param name="eq">Which equipment used</param>
        public Session(User user, Exercise exercise, long repetitions, Equipment eq)
        {
            User = user;
            Exercise = exercise;
            Repetitions = repetitions;
            Equipment = eq;
        }

        /// <summary>
        /// Hashtable representation of a session
        /// </summary>
        /// <returns>Hashtable</returns>
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

        /// <summary>
        /// Equipment used
        /// </summary>
        public Equipment Equipment
        {
            get { return equipment; }
            set { equipment = value; }
        }

        /// <summary>
        /// Number of repetitions
        /// </summary>
        public long Repetitions
        {
            get { return repetitions; }
            set { repetitions = value; }
        }

        /// <summary>
        /// Exercise performed during this session
        /// </summary>
        public Exercise Exercise
        {
            get { return exercise; }
            set { exercise = value; }
        }

        /// <summary>
        /// User of this session.
        /// </summary>
        public User User
        {
            get { return user; }
            set { user = value; }
        }

        /// <summary>
        /// Unique id
        /// </summary>
        public long Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}

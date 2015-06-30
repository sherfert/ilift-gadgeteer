using System.Collections;

namespace ilift.Model
{
    /// <summary>
    /// Represents the Equipment Type which can contain different Exercises
    /// </summary>
    public class EqType
    {
        private long id;
        private string name;
        private Exercise[] availableExercises;
        
        /// <summary>
        /// Constructor that constructs an EqType out of a Hashtable
        /// </summary>
        /// <param name="hashtable"></param>
        public EqType(Hashtable hashtable)
        {
            Id = (long) hashtable["id"];
            Name = (string) hashtable["name"];
            ArrayList listOfExercises = (ArrayList) hashtable["availableExercises"];
            AvailableExercises = new Exercise[listOfExercises.Count];
            for (int i = 0; i < listOfExercises.Count; i++)
            {
                AvailableExercises[i] = new Exercise((Hashtable) listOfExercises[i]);
            }
         
        }
        
        /// <summary>
        /// Get Equipment Type Id
        /// </summary>
        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Get Equipment Type Name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Retunrs all available exercises
        /// </summary>
        public Exercise[] AvailableExercises
        {
            get { return availableExercises; }
            set { availableExercises = value; }
        }
    }
}
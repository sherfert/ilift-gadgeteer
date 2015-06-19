using System.Collections;

namespace ilift.Model
{
    public class EqType
    {
        private long id;
        private string name;
        private Exercise[] availableExercises;
        
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

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Exercise[] AvailableExercises
        {
            get { return availableExercises; }
            set { availableExercises = value; }
        }
    }
}
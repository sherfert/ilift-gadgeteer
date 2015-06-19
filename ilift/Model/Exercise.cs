using System.Collections;

namespace ilift.Model
{
    public class Exercise
    {
        private long id;
        private string name;

        public Exercise(Hashtable hashtable)
        {
            Id = (long) hashtable["id"];
            Name = (string) hashtable["name"];
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
    }
}
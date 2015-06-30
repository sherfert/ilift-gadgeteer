using System.Collections;

namespace ilift.Model
{
    /// <summary>
    /// Represent the Exercise
    /// </summary>
    public class Exercise
    {
        private long id;
        private string name;

        /// <summary>
        /// Construct an Exercise out of a Hashtable
        /// </summary>
        /// <param name="hashtable"></param>
        public Exercise(Hashtable hashtable)
        {
            Id = (long) hashtable["id"];
            Name = (string) hashtable["name"];
        }

        /// <summary>
        /// The unique Id of the exercise
        /// </summary>
        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// The name of the exercise
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Hashtable representation out of the object 
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
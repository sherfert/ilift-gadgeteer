using System;
using Microsoft.SPOT;
using System.Collections;

namespace ilift.Patterns
{
    /// <summary> 
    /// The exercise manager that is responsible for retrieving 
    /// exercise from server
    /// </summary>
    public class ExerciseManager
    {
        /// <summary>
        /// All available exercises
        /// </summary>
        private static Hashtable _exercises = new Hashtable();

        /// <summary>
        /// The constructor that initialize all supported exercises
        /// </summary>
        static ExerciseManager()
        {
            _exercises[1L] = new BicepCurl();
            _exercises[2L] = new LateralRaise();
            _exercises[3L] = new TricepExtension();
        }

        /// <summary>
        /// Fetching the exercise by id
        /// </summary>
        /// <param name="id"> Exercise Id </param>
        /// <return> The exercise object </return>
        public static AbstractExercise GetExercise(long id)
        {
            return (AbstractExercise)_exercises[id];
        }

        
    }
}

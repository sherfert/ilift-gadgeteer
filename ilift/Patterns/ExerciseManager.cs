using System;
using Microsoft.SPOT;
using System.Collections;

namespace ilift.Patterns
{
    public class ExerciseManager
    {
        private static Hashtable _exercises = new Hashtable();

        static ExerciseManager()
        {
            _exercises[1L] = new BicepCurl();
            _exercises[2L] = new LateralRaise();
            _exercises[3L] = new TricepExtension();
        }

        public static AbstractExercise GetExercise(long id)
        {
            return (AbstractExercise)_exercises[id];
        }

        
    }
}

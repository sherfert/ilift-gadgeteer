using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;
using ilift.Patterns;

namespace ilift.Controller
{
    class ExecuteExerciseState:ExecutionState
    {
        public ExecuteExerciseState(DisplayTE35 display, StateManager state, AbstractExercise exercise)
            : base(display, state)
        {

        }
        public override void init()
        {
            throw new NotImplementedException();
        }

        public override void finish()
        {
            throw new NotImplementedException();
        }
    }
}

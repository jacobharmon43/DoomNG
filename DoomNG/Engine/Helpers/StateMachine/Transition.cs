using System;

namespace FSA
{
    /* How to use:
     * 
     * Nothing here is that crazy, it just stores logic and a state.
     * 
     */

    public class Transition
    {
        public State NextState { get; private set; }
        private Func<bool> condition;

        public Transition(Func<bool> condition, State nextState)
        {
            this.condition = condition;
            this.NextState = nextState;
        }

        public bool Check()
        {
            if(condition == null) return false;
            return condition.Invoke();
        }
    }
}

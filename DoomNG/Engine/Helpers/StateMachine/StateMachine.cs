using System.Collections.Generic;

namespace FSA
{
    /* How to use:
     * 
     * Once you have set up the statemachine either by meticulously constructing it, or using the builder I wrote
     * Just run RunStateMachine() in the Update() method, or call it whenever, it mostly does its own managing and logic
     * it doesn't really care when
     * 
     */

    public class StateMachine
    {
        public State CurrentState { get; private set; }
        public float TimeInCurrentState { get; private set; }
        private State _startingState;

        private List<Transition> _anyStates = new List<Transition>();

        public StateMachine(State startingState)
        {
            CurrentState = startingState;
            _startingState = startingState;
            CurrentState.InitializeState();
        }

        public void ResetStateMachine(){
            CurrentState = _startingState;
        }

        public void RunStateMachine(float deltaTime)
        {
            CurrentState.OnUpdate();
            State tmp = QueryStateChange();   
            if(tmp && tmp != CurrentState)
            {
                ChangeState(tmp);
            }
            TimeInCurrentState += deltaTime;
        }

        public void AddAnyState(Transition t)
        {
            _anyStates.Add(t);
        }

        private State QueryStateChange()
        {
            foreach(Transition t in _anyStates)
            {
                if(t.Check())
                {
                    return t.NextState;
                }
            }
            return CurrentState.QueryStateChange();;
        }

        private void ChangeState(State nextState) //Calls exit methods and initialization methods of new machine
        {
            if (CurrentState == nextState || nextState == null)
            {
                return;
            }
            CurrentState.OnExit();
            CurrentState = nextState;
            CurrentState.InitializeState();
            TimeInCurrentState = 0;
        }
    }
}

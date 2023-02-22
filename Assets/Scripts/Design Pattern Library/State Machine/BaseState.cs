namespace FiniteStateMachine
{
    /// <summary>
    /// Base state for the FSM (finite state machine).
    /// </summary>
    public class BaseState
    {
        /// <summary>
        /// State name.
        /// </summary>
        protected string _name;

        /// <summary>
        /// FSM that this state is part of.
        /// </summary>
        protected FSM _fsm;

        /// <summary>
        /// State constructor.
        /// </summary>
        /// <param name="name">Name of the state.</param>
        /// <param name="fsm">Finite state machine.</param>
        public BaseState(string name, FSM fsm)
        {
            _name = name;
            _fsm = fsm;
        }

        /// <summary>
        /// Handle logic when entering state.
        /// </summary>
        public virtual void Enter() { }

        /// <summary>
        /// Handle logic when exiting state.
        /// </summary>
        public virtual void Exit() { }

        /// <summary>
        /// Handle logic while in this state.
        /// </summary>
        public virtual void HandleState() { }
    }
}

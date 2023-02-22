using UnityEngine;

namespace FiniteStateMachine
{
    /// <summary>
    /// Finite State Machine pattern.
    /// </summary>
    public class FSM : MonoBehaviour
    {
        /// <summary>
        /// The current state.
        /// </summary>
        private BaseState currentState;

        /// <summary>
        /// Gets the initial state if there is one.
        /// </summary>
        private void Start()
        {
            currentState = GetInitialState();
            currentState?.Enter();
        }

        /// <summary>
        /// Handles the state behavior.
        /// </summary>
        private void Update() => currentState?.HandleState();

        /// <summary>
        /// Changes state by exiting the old one and entering the new one.
        /// </summary>
        /// <param name="newState">The new state.</param>
        public void ChangeState(BaseState newState)
        {
            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }

        /// <summary>
        /// Get the set initial state for this FSM.
        /// </summary>
        protected virtual BaseState GetInitialState() => null;
    }
}
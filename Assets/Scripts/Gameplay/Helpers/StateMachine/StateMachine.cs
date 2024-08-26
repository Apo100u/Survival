using System;
using System.Collections.Generic;

namespace SurvivalGame.Gameplay.Helpers.StateMachine
{
    public class StateMachine<TCommand, TState> where TCommand : Enum where TState : State<TCommand>
    {
        private State<TCommand> currentState;
        private Dictionary<Type, TState> statesByTypes = new();

        public void Start(TState initialState)
        {
            currentState = initialState;
            currentState.OnEnter();
        }

        public void Process()
        {
            currentState.Process();
        }

        public void AddState(TState state)
        {
            statesByTypes.Add(state.GetType(), state);
        }

        public void ExecuteCommand(TCommand command)
        {
            if (currentState.transitions.TryGetValue(command, out Type newStateType))
            {
                currentState.conditions.TryGetValue(newStateType, out Func<bool> condition);
                bool conditionSatisfied = condition == null || condition.Invoke();

                if (conditionSatisfied)
                {
                    ChangeState(statesByTypes[newStateType]);
                }
            }
        }

        private void ChangeState(State<TCommand> newState)
        {
            currentState.OnExit();
            currentState = newState;
            currentState.OnEnter();
        }
    }
}
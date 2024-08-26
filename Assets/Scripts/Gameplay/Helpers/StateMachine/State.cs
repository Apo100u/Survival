using System;
using System.Collections.Generic;

namespace SurvivalGame.Gameplay.Helpers.StateMachine
{
    public abstract class State<TCommand> where TCommand : Enum
    {
        public readonly Dictionary<TCommand, Type> transitions = new();
        public readonly Dictionary<Type, Func<bool>> conditions = new();

        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExit()
        {
            
        }

        public virtual void Process()
        {
            
        }

        public State<TCommand> AddTransition<TState>(TCommand command, Func<bool> condition = null) where TState : State<TCommand>
        {
            transitions.Add(command, typeof(TState));
            conditions.Add(typeof(TState), condition);
            return this;
        }
    }
}
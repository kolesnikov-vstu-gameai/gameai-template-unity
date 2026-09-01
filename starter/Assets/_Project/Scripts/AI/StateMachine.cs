using System;
using System.Collections.Generic;

namespace GameAI.AI
{
    /// <summary>Минимальный конечный автомат без зависимости от UnityEngine — тестируется в EditMode.</summary>
    public sealed class StateMachine<TState> where TState : Enum
    {
        private readonly Dictionary<(TState, string), TState> _transitions = new();
        public TState Current { get; private set; }
        public event Action<TState, TState> OnTransition;

        public StateMachine(TState initial) => Current = initial;

        public void AddTransition(TState from, string trigger, TState to) => _transitions[(from, trigger)] = to;

        public bool Fire(string trigger)
        {
            if (!_transitions.TryGetValue((Current, trigger), out var next)) return false;
            var prev = Current;
            Current = next;
            OnTransition?.Invoke(prev, next);
            return true;
        }
    }
}

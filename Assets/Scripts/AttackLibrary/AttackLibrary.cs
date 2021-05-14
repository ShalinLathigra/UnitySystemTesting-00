using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using States;

namespace AttackLibrary
{
    public class AttackLibrary
    {
        public enum AttackInput
        {
            Default=-1,
        
        }

        public AttackInput lastInput { get; private set; }
        public Attack lastAttack { get; private set; }
        private Queue<Attack> _activeChain;
        private Dictionary<AttackInput, Queue<Attack>> _inputToChainDict;
        private Dictionary<Attack, List<Transition>> _attackTransitionDict;
        private Dictionary<AttackInput, List<Transition>> _inputTransitionDict;
        private List<Transition> _anyChainTransitions;
        private List<Transition> EmptyTransitions = new List<Transition>(0);

        private List<Transition> _activeAttackTransitions;
        private List<Transition> _activeInputTransitions;

        public AttackLibrary()
        {
            lastInput = AttackInput.Default;
            _activeChain = new Queue<Attack>();
            _inputToChainDict = new Dictionary<AttackInput, Queue<Attack>>();
            _attackTransitionDict = new Dictionary<Attack, List<Transition>>();
            _inputTransitionDict = new Dictionary<AttackInput, List<Transition>>();
            _anyChainTransitions = new List<Transition>();

            _activeAttackTransitions = EmptyTransitions;
            _activeInputTransitions = EmptyTransitions;
        }

        public bool RequestAttack(AttackInput input, [CanBeNull] out Attack nextAttack)
        {
            var valid = false;
            if (lastInput == AttackInput.Default || _activeChain.Count == 0)   // If not in an attack chain || last chain over 
            {
                valid = _inputToChainDict.TryGetValue(input, out var newList);
                if (valid)
                    _activeChain = newList;
            }
            else if (input == lastInput) // Continue the chain.
            {
                valid = true;
            }
            else
            {
                var t = CheckTransitions(input, out var transition);

                if (t)
                {
                    valid = true;
                    _activeChain = transition.toChain;
                    if (transition.appendToList)
                    {
                        var tmp = _inputToChainDict.TryGetValue(input, out var baseQueue);
                        if (tmp)
                        {
                            _activeChain = new Queue<Attack>(_activeChain.Concat(baseQueue));
                        }
                    }
                }
                else
                {
                    valid = _inputToChainDict.TryGetValue(input, out var newList);
                    if (valid)
                        _activeChain = newList;
                }
            }
            // update 
            nextAttack = _activeChain?.Peek();
            _activeChain?.Dequeue();
        
            lastInput = (_activeChain?.Count == 0) ? AttackInput.Default : input;
            lastAttack = nextAttack;
            // _activeAttackTransitions from _attackTransitionDict [nextAttack]
            // _activeInputTransitions from _inputTransitionDict
            return valid;
        }

        private bool CheckTransitions(AttackInput _newInput, out Transition _t)
        {
            foreach (var t in _activeAttackTransitions.Where(t => t.predicate()))
            {
                _t = t;
                return true;
            }
        
            foreach (var t in _activeInputTransitions.Where(t => t.predicate()))
            {
                _t = t;
                return true;
            }
        
            foreach (var t in _anyChainTransitions.Where(t => t.predicate()))
            {
                _t = t;
                return true;
            }

            _t = default;
            return false;
        }

        public void AddTransition(Attack from, Queue<Attack> to, Func<bool> pred)
        {
            var exists = _attackTransitionDict.TryGetValue(from, out var t);
            if (!exists)
            {
                t = new List<Transition>();
                _attackTransitionDict[from] = t;
            }
            _attackTransitionDict[from].Add(new Transition(to, pred));
        }
    
        public void AddTransition(AttackInput from, Queue<Attack> to, Func<bool> pred)
        {
            var exists = _inputTransitionDict.TryGetValue(from, out var t);
            if (!exists)
            {
                t = new List<Transition>();
                _inputTransitionDict[from] = t;
            }
            _inputTransitionDict[from].Add(new Transition(to, pred));
        }
        public void AddAnyTransition(Queue<Attack> to, Func<bool> pred)
        {
            _anyChainTransitions.Add(new Transition(to, pred));
        }
    
    
        private struct Transition
        {
            public Queue<Attack> toChain;
            public readonly Func<bool> predicate;
            public readonly bool appendToList;

            public Transition(Queue<Attack> toChain, Func<bool> predicate, bool appendToList = false)
            {
                this.toChain = toChain;
                this.predicate = predicate;
                this.appendToList = appendToList;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using States;
using UnityEngine;

namespace AttackLibrary
{
    public class AttackLibrary
    {
        public enum AttackInput
        {
            Default=-1,
            Hit1=0,
            Hit2=1,
            Hit3=2,
            Hit4=3,
            Hit5=4,
            Hit6=5
        }

        public AttackInput lastInput { get; private set; }
        public AttackInput currentInput { get; private set; }
        public Attack lastAttack { get; private set; }
        private List<Attack> _activeChain;
        private Dictionary<AttackInput, List<Attack>> _inputToChainDict;
        private Dictionary<Attack, List<Transition>> _attackTransitionDict;
        private Dictionary<AttackInput, List<Transition>> _inputTransitionDict;
        private List<Transition> _anyChainTransitions;
        private List<Transition> EmptyTransitions = new List<Transition>(0);

        private List<Transition> _activeAttackTransitions;
        private List<Transition> _activeInputTransitions;
        private int chainIndex;

        public AttackLibrary()
        {
            lastInput = AttackInput.Default;
            currentInput = AttackInput.Default;
            _activeChain = new List<Attack>();
            _inputToChainDict = new Dictionary<AttackInput, List<Attack>>();
            _attackTransitionDict = new Dictionary<Attack, List<Transition>>();
            _inputTransitionDict = new Dictionary<AttackInput, List<Transition>>();
            _anyChainTransitions = new List<Transition>();

            _activeAttackTransitions = EmptyTransitions;
            _activeInputTransitions = EmptyTransitions;

            chainIndex = 0;
        }

        public void AddInputAttackChain(AttackInput index, List<Attack> chain)
        {
            if (!_inputToChainDict.ContainsKey(index))
                _inputToChainDict[index] = chain;
        }

        public bool RequestAttack(AttackInput input, [CanBeNull] out Attack nextAttack)
        {
            bool valid;
            currentInput = input;
            if (lastInput == AttackInput.Default || _activeChain.Count == 0)   // If not in an attack chain || last chain over 
            {
                valid = _inputToChainDict.TryGetValue(input, out var newList);
                if (valid)
                {
                    _activeChain = newList;
                    chainIndex = 0;
                }
                else
                {
                    nextAttack = null;
                    return false;
                }
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
                    chainIndex = 0;
                    if (transition.appendToList)
                    {
                        var tmp = _inputToChainDict.TryGetValue(input, out var baseQueue);
                        if (tmp)
                        {
                            _activeChain = new List<Attack>(_activeChain.Concat(baseQueue));
                        }
                    }
                }
                else
                {
                    valid = _inputToChainDict.TryGetValue(input, out var newQueue);
                    if (valid)
                    {
                        _activeChain = newQueue;
                        chainIndex = 0;
                    }
                }
            }
            // update 
            nextAttack = _activeChain?[chainIndex];
            chainIndex += 1;
        
            lastInput = _activeChain == null || (chainIndex >= _activeChain.Count) ? AttackInput.Default : input;
            lastAttack = nextAttack;
            
            _activeAttackTransitions = (_attackTransitionDict.TryGetValue(lastAttack, out var attackTs)) ? attackTs : EmptyTransitions;
            _activeInputTransitions = (_inputTransitionDict.TryGetValue(lastInput, out var inputTs)) ? inputTs : EmptyTransitions;
            
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

        public void AddTransition(Attack from, List<Attack> to, Func<bool> pred)
        {
            var exists = _attackTransitionDict.TryGetValue(from, out var t);
            if (!exists)
            {
                t = new List<Transition>();
                _attackTransitionDict[from] = t;
            }
            _attackTransitionDict[from].Add(new Transition(to, pred));
        }
    
        public void AddTransition(AttackInput from, List<Attack> to, Func<bool> pred)
        {
            var exists = _inputTransitionDict.TryGetValue(from, out var t);
            if (!exists)
            {
                t = new List<Transition>();
                _inputTransitionDict[from] = t;
            }
            _inputTransitionDict[from].Add(new Transition(to, pred));
        }
        public void AddAnyTransition(List<Attack> to, Func<bool> pred)
        {
            _anyChainTransitions.Add(new Transition(to, pred));
        }
    
    
        private struct Transition
        {
            public List<Attack> toChain;
            public readonly Func<bool> predicate;
            public readonly bool appendToList;

            public Transition(List<Attack> toChain, Func<bool> predicate, bool appendToList = false)
            {
                this.toChain = toChain;
                this.predicate = predicate;
                this.appendToList = appendToList;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using States;
using UnityEngine;

namespace AttackLibrary
{
    public class PlayerAttackLibrary : MonoBehaviour
    {
        private AttackLibrary _library;

        [SerializeField] public List<Attack> lightChain;
        [SerializeField] public List<Attack> heavyChain;

        private Dictionary<int, AttackLibrary.AttackInput> _inputMap;

        private static Queue<Attack> GenerateQueueFromList(IEnumerable<Attack> input) => new Queue<Attack>(input);

        // in this function, need to map provided inputs to attackLibrary outputs

        private void Awake()
        {
            _library = new AttackLibrary();
            InitInputMap();
            InitChains();
            InitTransitions();
        }

        private void InitInputMap()
        {
            _inputMap = new Dictionary<int, AttackLibrary.AttackInput>
            {
                [0] = AttackLibrary.AttackInput.Hit1,
                [1] = AttackLibrary.AttackInput.Hit2
            };
        }

        private void InitChains()
        {
            _library.AddInputAttackChain(_inputMap[0], lightChain);
            _library.AddInputAttackChain(_inputMap[1], heavyChain);
        }

        private void InitTransitions()
        {
            //_library.AddTransition(_inputMap[0], heavyChain, () => _library.currentInput == _inputMap[1]);
        }

        public bool RequestAttack(int input, out Attack ret)
        {
            if (_inputMap.TryGetValue(input, out var key))
            {
                if (_library.RequestAttack(key, out ret))
                {
                    return true;
                }
            }

            ret = null;
            return false;
        }
    }
}
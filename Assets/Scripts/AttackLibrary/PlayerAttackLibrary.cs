using System.Collections.Generic;
using States;
using UnityEngine;

namespace AttackLibrary
{
    public class PlayerAttackLibrary : MonoBehaviour
    {
        private AttackLibrary _library;

        [SerializeField] public List<Attack> LightChain;
        [SerializeField] public List<Attack> heavyChain;

        private void Awake()
        {
            _library = new AttackLibrary();
            // I can now say: For each attack in LightChain, add to 
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace BehaviourStateTree
{
    public class StateTree : MonoBehaviour
    {
        public bool active { get; set; }
        [FormerlySerializedAs("state")] public StateBranch state;
        LinkedList<StateBranch> history;    // End is newest

        protected void Set(StateBranch _state)
        {
            if (state == _state)
                return;
            Set(_state, true);
        }
    
        protected void Set(StateBranch _state, bool overRide)
        {
            if (state != null)
                state.Exit();
            state = _state;
            state.Enter();
        }

        public string GetStatePathString()
        {
            return GetType() + " -> " +  state?.GetStatePathString();
        }
    }
}

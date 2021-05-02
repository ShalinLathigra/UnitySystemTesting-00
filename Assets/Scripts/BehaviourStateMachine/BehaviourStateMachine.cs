using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BehaviourStateMachine : MonoBehaviour
{
    public bool active { get; set; }
    [HideInInspector] public BehaviourState state;
    LinkedList<BehaviourState> history;    // End is newest

    protected void Set(BehaviourState _state)
    {
        if (state == _state)
            return;
        else
            Set(_state, true);
    }
    
    protected void Set(BehaviourState _state, bool overRide)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BehaviourStateMachine : MonoBehaviour
{
    public bool active { get; set; }
    [HideInInspector] public BehaviourState state;
    LinkedList<IBehaviourState> history;    // End is newest

    protected void Set(BehaviourState _state)
    {
        if (state == _state)
            return;
        else
        {
            if (state != null)
                state.Exit();
            state = _state;
            state.Enter();
        }
    }
    
    protected void Set(BehaviourState _state, BehaviourState _subState)
    {
        Set(_state);
        state.Set(_subState);
    }
}

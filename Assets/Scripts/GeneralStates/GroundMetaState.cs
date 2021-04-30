using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMetaState : BehaviourState
{
    [SerializeField] private Move move;
    [SerializeField] private Idle idle;
    // Also needs reference to the input mechanism
    public override void Enter()
    {
        complete = false;
        Set(idle);
    }

    public override void FixedDo()
    {
        if (core.input.shouldJump)
            core.SetAirState();

        if (core.input.shouldMove)
            Set(move);
        else
            Set(idle);

        state.FixedDo();
    }

    public override void Exit()
    {
        if (state != null)
            state.Exit();
        //Debug.Log(name + " Exited State: " + GetType());
    }
}

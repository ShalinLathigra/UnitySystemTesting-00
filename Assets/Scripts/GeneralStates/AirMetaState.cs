using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMetaState : BehaviourState
{
    // TODO: Need to include: Velocity modification in air (similar, but not equal to the way it works on ground (not snapping))
    //* Jumping should use a similar animation curve, and the state should have a lot more ways to interrupt it
    //*     i.e. Wall jumping, hitting a rope, etc.
    //* If animation curve FINISHES: stop updating vel
    //* If input STOPS: end Jump, initiate fall
    //*     Maybe JUMP and FALL are just implicitly handled by the AirControl?
    //*     * Should have a separate method to just add to velocity in the air, maybe with rb.AddForce?
    [SerializeField] Jump jump;
    [SerializeField] Fall fall;
    public override void Enter()
    {
        Debug.Log("Entered Air State");
        if (core.input.shouldJump)
            Set(jump);
        else
            Set(fall);
    }
    
    public override void FixedDo()
    {
    }
    public override void Exit()
    {
    }
}

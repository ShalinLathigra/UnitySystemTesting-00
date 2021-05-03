using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : BehaviourState
{
    AirSO coreAir => ((IAirEntity)core).airSO;
    protected AnimationCurve jumpCurve => coreAir.jumpCurve;
    protected float duration => jumpCurve.keys[jumpCurve.keys.Length - 1].time;

    private float completeTime = 0.0f;
    public bool extendedJump => (completeTime >= startTime + duration);
    private bool canEndJump => Time.time - startTime > duration * 0.4f;

    public override void Enter()
    {
        base.Enter();
    }
    
    public override void FixedDo()
    {
        core.rb.velocity = new Vector2(
            core.rb.velocity.x, 
            jumpCurve.Evaluate(Time.time - startTime) * coreAir.maxJumpSpeed
            );

        complete = (Time.time > (startTime + duration)) || (core.input.jumpCancelled && canEndJump);
    }

    public override void Exit()
    {
        base.Exit();
        completeTime = Time.time;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

using BStateMachine;


public class AirMetaState : BehaviourState
{
    [SerializeField] Jump jump;
    [SerializeField] Fall fall;
    IAirEntity coreEntity => (IAirEntity) core;
    AirSO coreAir => coreEntity.airSO;

    public bool landing => state == fall && state.complete;
    public override void Enter()
    {
        base.Enter();
        if (coreEntity.shouldJump && coreEntity.canJump && state != jump)
        {
                Set(jump, true);
        }
        else if (state != jump)
        {
            Set(fall);
            fall.SetFall(false);
        }
    }
    
    // Should handle side to side motion in AirControl
    public override void FixedDo()
    {
        // How can I affect the vel like this?
        core.rb.velocity = math.lerp(
            core.rb.velocity, 
            new Vector2(core.input.horizontalInput * coreAir.maxAirSpeed, 0.0f),
            Mathf.Clamp(Time.time - startTime, 0.0f, 1.0f)
            );

        state.FixedDo();

        if (state.complete)
        {
            if (state == jump)
            {
                Set(fall);
                fall.SetFall(jump.extendedJump);
            }
            else if (state == fall)
            {
                complete = true;
            }
        }
    }
    public override void Exit()
    {
        base.Exit();
        state = null;
    }
}

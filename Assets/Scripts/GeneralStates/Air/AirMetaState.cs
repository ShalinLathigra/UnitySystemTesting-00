using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

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
    IAirEntity coreEntity => (IAirEntity) core;
    AirSO coreAir => coreEntity.airSO;
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
        state.FixedDo();

        // Lerp velocity towards the input direction?
        core.rb.velocity = math.lerp(
            core.rb.velocity, 
            new Vector2(core.input.horizontalInput * coreAir.maxAirSpeed, 0.0f),
            coreAir.moveTransition
            );

        if (state.complete)
        {
            if (state == jump)
            {
                //if (jump.extendedJump)
                //TODO: Set long fall in fall state based on jmp completion
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

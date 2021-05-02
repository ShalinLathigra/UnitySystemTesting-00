using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

/*
* Summary: Move
* This State is used by all objects moving SIDE TO SIDE
* Clamps motion to the grid, can be used by other Meta-States
*/
public class Move : BehaviourState
{
    //* Contains all of the important fields modifying this behaviour
    MoveSO coreMove => ((IMoveEntity)core).moveSO;

    protected Vector2 stateVelocity;
    public override void Enter()
    {
        base.Enter();
        stateVelocity = new Vector2(core.rb.velocity.x, 0.0f);
    }
    public override void FixedDo()
    {
        // should store stateVelocity
        stateVelocity = new Vector2(
            core.input.horizontalInput * coreMove.maxSpeed * coreMove.accelCurve.Evaluate(Time.time - startTime), 
            0.0f
            );
        
        Vector2 alignWithGround = new Vector2(
            core.spatial.groundNormal.y,
            -core.spatial.groundNormal.x
        );
        
        // Not a hard set to smooth out the transition
        core.rb.velocity = math.lerp(core.rb.velocity, stateVelocity.x * alignWithGround, coreMove.accelCurve.Evaluate(Time.time - startTime));

        // Completion Check
        complete = !(core.input.shouldMove && core.spatial.grounded);
    }
}
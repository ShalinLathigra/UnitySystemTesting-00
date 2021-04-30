using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
* Summary: Move
* This State is used by all objects moving SIDE TO SIDE
* Clamps motion to the grid, can be used by other Meta-States
*/
public class Move : BehaviourState
{
    //* Contains all of the important fields modifying this behaviour
    [SerializeField] private MoveSO moveSO;

    protected Vector2 stateVelocity;
    public override void Enter()
    {
        complete = false;
        stateVelocity = Vector2.zero;
        startTime = Time.time;
    }
    public override void FixedDo()
    {
        // should store stateVelocity
        stateVelocity = new Vector2(
            core.input.horizontalInput * moveSO.maxSpeed * moveSO.accelCurve.Evaluate(Time.time - startTime), 
            0.0f
            );
        
        Vector2 alignWithGround = new Vector2(
            core.spatial.groundNormal.y,
            -core.spatial.groundNormal.x
        );

        if (core.spatial.distanceToGround > core.spatial.skinWidth)
        {
            float distance = core.spatial.distanceToGround - core.spatial.skinWidth;
            core.rb.MovePosition(core.rb.position - new Vector2(0.0f, distance));
        }
        core.rb.velocity = stateVelocity.x * alignWithGround;

        // Completion Check
        complete = !(core.input.shouldMove && core.spatial.grounded);
    }
    public override void Exit()
    {
    }
}
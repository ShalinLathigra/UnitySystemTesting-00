using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : BehaviourState
{
    public override void Enter()
    {
        //TODO: Debug why Jump only triggers once. Primary suspect is shouldJump
        Debug.Log("Entered Jump " + Time.time);
        core.rb.velocity = Vector2.up * 10.0f;
    }
    public override void FixedDo()
    {
    }
    public override void Finish()
    {
    }
    public override void Exit()
    {
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : HybridCharacter
{
    [Header("Available States")]
    [SerializeField] private GroundMetaState groundMeta;
    [SerializeField] private AirMetaState airMeta;
    
    public override bool canJump { get { return Time.time - spatial.timeLastGrounded < 0.1f; } }
    public override bool airComplete { get { return (spatial.grounded && airMeta.complete); } }
    public override bool shouldJump { get { return input.shouldJump; } }

    protected override void Awake() {
        base.Awake();
        Set(groundMeta);
    }

    protected override void FixedUpdate() {
        if ((!airComplete) || (canJump && input.shouldJump))   // Need to block this out when you initially start jumping!
        {
            Set(airMeta);
        }   
        else if (spatial.grounded)
        {
            Set(groundMeta);
        }

        base.FixedUpdate();
    }

    public override void SetAirState()
    {
        Set(airMeta);
    }
}

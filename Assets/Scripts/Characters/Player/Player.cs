using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : HybridCharacter
{
    [Header("Available States")]
    [SerializeField] private GroundMetaState groundMeta;
    [SerializeField] private AirMetaState airMeta;

    //TODO: Connect Sound Effects
    //TODO: Connect Animations
    //TODO: Create some sort of IAnimator and ISoundPlayer that can be referenced by the states to control sounds + anims
    //TODO: as far as the Animator goes, this is where I'll need to start considering the different collider types
    //TODO: Animator should be a whole different Part that can be attached, and reads from a scriptable object animations passed in by states. 
    //TODO: AnimObjects contain: maybe a dict mapping each frame (key = i {0, anim length}) to {sprite, hit/hurt/etc boxes}
    public override bool canJump { get { return Time.time - spatial.timeLastGrounded < 0.1f; } }
    public override bool airComplete { get { return (spatial.grounded && airMeta.complete); } }
    public override bool shouldJump { get { return input.shouldJump; } }

    protected override void Awake() {
        base.Awake();
        Set(groundMeta);
    }

    protected override void FixedUpdate() {
        // Need to block this out when you initially start jumping!
        if ((!airComplete) || (canJump && input.shouldJump))
        {
            Set(airMeta);
        }   
        else
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

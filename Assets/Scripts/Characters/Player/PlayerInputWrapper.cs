using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputWrapper : InputWrapper
{
    /* 
    * SUMMARY: Exposes input from the PlayerInput to other components
    TODO: Figure out what OTHER input I want available, expose it through here
    */
    protected bool _shouldMove;
    protected Vector2 _directionalInput;


    [SerializeField] protected float jumpBufferTime;
    private float timeOfLastJumpInput = -100.0f;

    // Should store: Time of last call
    // How much leeway the player has

    public override bool shouldMove { get { return _shouldMove; } }
    public override bool shouldJump     // Jump Buffering
    { 
        get { 
        return timeOfLastJumpInput + jumpBufferTime >= Time.time; 
        } 
    }
    

    public override Vector2 directionalInput { get { return _directionalInput; } }
    public override float horizontalInput { get { return directionalInput.x; } }
    public override float verticalInput { get { return directionalInput.y; } }
    
    private void Awake() {
        _directionalInput = new Vector2();
        _shouldMove = false;
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 newInput = context.ReadValue<Vector2>();
        _directionalInput = newInput;
        _shouldMove = _directionalInput != Vector2.zero;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
            timeOfLastJumpInput = Time.time;
    }
}

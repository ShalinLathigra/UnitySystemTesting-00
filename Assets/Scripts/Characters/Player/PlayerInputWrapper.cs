using Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Characters.Player
{
    public class PlayerInputWrapper : InputWrapper
    {
        /* 
         * SUMMARY: Exposes input from the PlayerInput to other components
         */
        private bool _jumpCancelled;
        private bool _shouldMove;
        private Vector2 _directionalInput;


        [SerializeField] protected float jumpBufferTime;
        private float _timeOfLastJumpInput = -100.0f;

        // Should store: Time of last call
        // How much leeway the player has

        public override bool shouldMove => _shouldMove;
        public override bool jumpCancelled => _jumpCancelled;

        public override bool shouldJump => _timeOfLastJumpInput + jumpBufferTime >= Time.time;


        public override Vector2 directionalInput => _directionalInput;
        public override float horizontalInput => directionalInput.x;
        public override float verticalInput => directionalInput.y;

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
            if (!context.canceled)
            {
                _timeOfLastJumpInput = Time.time;
                _jumpCancelled = false;
            }
            else
            {
                _jumpCancelled = true;
            }
        }
    }
}

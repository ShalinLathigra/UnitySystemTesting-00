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
        private bool _shouldMove;
        private Vector2 _directionalInput;
        private bool _jumpCancelled;
        private int _lastAttackKey;


        [SerializeField] protected float jumpBufferTime;
        private float _timeOfLastJumpInput = -100.0f;

        [SerializeField] protected float attackBufferTime;
        private float _timeOfLastAttackInput = -100.0f;
        
        public override bool shouldMove => _shouldMove;

        public override bool shouldJump => _timeOfLastJumpInput + jumpBufferTime >= Time.time;
        public override bool shouldAttack => _timeOfLastAttackInput + attackBufferTime >= Time.time;
        
        public override bool jumpCancelled => _jumpCancelled;
        public override int lastAttackKey => _lastAttackKey;
        


        public override Vector2 directionalInput => _directionalInput;
        public override float horizontalInput => directionalInput.x;
        public override float verticalInput => directionalInput.y;

        private void Awake() {
            _directionalInput = new Vector2();
            _shouldMove = false;
            _lastAttackKey = 0;
        }

        public override void Move(InputAction.CallbackContext context)
        {
            var newInput = context.ReadValue<Vector2>();
            _directionalInput = newInput;
            _shouldMove = _directionalInput != Vector2.zero;
        }

        public override void Jump(InputAction.CallbackContext context)
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

        public void Strike1(InputAction.CallbackContext context)
        {
            if (context.canceled) return;
            _timeOfLastAttackInput = Time.time;
            _lastAttackKey = 0;
        }
        public void Strike2(InputAction.CallbackContext context)
        {
            if (context.canceled) return;
            _timeOfLastAttackInput = Time.time;
            _lastAttackKey = 1;
        }
        public void Strike3(InputAction.CallbackContext context)
        {
            if (context.canceled) return;
            _timeOfLastAttackInput = Time.time;
            _lastAttackKey = 2;
        }
    }
}

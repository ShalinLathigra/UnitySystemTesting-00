using UnityEngine;
using UnityEngine.InputSystem;

namespace Components
{
    public abstract class InputWrapper : MonoBehaviour
    {
        public virtual bool shouldMove { get; }
        public virtual bool shouldJump { get; }
        public abstract bool shouldAttack { get; }
        public virtual bool jumpCancelled { get; }
        public virtual Vector2 directionalInput { get; }
        public virtual float horizontalInput { get; }
        public virtual float verticalInput { get; }
        public virtual int lastAttackKey { get; }

        public virtual void Move(InputAction.CallbackContext context) {}

        public virtual void Jump(InputAction.CallbackContext context) {}

    }
}
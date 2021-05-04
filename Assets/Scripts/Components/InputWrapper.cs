using UnityEngine;

namespace Components
{
    public class InputWrapper : MonoBehaviour
    {
        public virtual bool shouldMove { get; }
        public virtual bool shouldJump { get; }
        public virtual bool jumpCancelled { get; }
        public virtual Vector2 directionalInput { get; }
        public virtual float horizontalInput { get; }
        public virtual float verticalInput { get; }
    
    }
}
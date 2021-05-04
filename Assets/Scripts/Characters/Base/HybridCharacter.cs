using UnityEngine;

namespace Characters.Base
{
    public abstract class HybridCharacter : GroundCharacter, IAirEntity
    {
        [SerializeField] private AirSO _airSO;
        public AirSO airSO { get { return _airSO; } }
        public abstract bool canJump { get; }
        public abstract bool airComplete { get; }
        public abstract bool shouldJump { get; }
    }
}

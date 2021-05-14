using States;
using UnityEngine;

namespace Characters.Base
{
    public abstract class GroundCharacter : Character, IMoveEntity
    {
        [SerializeField] private MoveSO _moveSO;
        public MoveSO moveSO { get{ return _moveSO; } }
    }
}

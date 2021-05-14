using UnityEngine;

namespace States
{
    [CreateAssetMenu(fileName = "MoveSO", menuName = "PrototypeProject/CharacterSO/MoveSO")]
    public class MoveSO : ScriptableObject 
    {
        public AnimationCurve accelCurve; 
        public float maxSpeed;
        public float maxSlope;
    }
}
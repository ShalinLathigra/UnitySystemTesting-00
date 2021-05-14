using UnityEngine;

namespace States
{
    [CreateAssetMenu(fileName = "AirSO", menuName = "PrototypeProject/CharacterSO/AirSO")]
    public class AirSO : ScriptableObject 
    {
        public AnimationCurve jumpCurve; 
        public AnimationCurve longFallCurve; 
        public AnimationCurve shortFallCurve;
        public float maxAirSpeed;
        public float maxJumpSpeed;
    }
}
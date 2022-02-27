using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(menuName = "PrototypeProject/Dialogue/FloatingDialoguePreferences", fileName = "FloatingDialoguePreferences", order = 4)]
    public class FloatingDialoguePreferences : ScriptableObject
    {
        [SerializeField] private AnimationCurve displacementCurve;
        public AnimationCurve displacement => displacementCurve;
        [SerializeField] private AnimationCurve alphaCurve;
        public AnimationCurve alpha => alphaCurve;
        [SerializeField] private AnimationCurve wordCurve;
        public AnimationCurve words => wordCurve;
        [SerializeField] private float minFloatTextDuration;
        public float minDuration => minFloatTextDuration;
    }
}

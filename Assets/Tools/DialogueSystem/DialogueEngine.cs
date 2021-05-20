using UnityEngine;

namespace DialogueSystem
{
    public class DialogueEngine : MonoBehaviour
    {
        // Basically, each NPC will carry around a reference to their own dialogue tree.
        // If the player is near you, not holding another input, and 

        private DialogueTree _currentTree; 
        public static DialogueEngine d { get; private set; }

        [SerializeField] private AnimationCurve displacementCurve;
        public AnimationCurve displacement => displacementCurve;
        [SerializeField] private AnimationCurve alphaCurve;
        public AnimationCurve alpha => alphaCurve;
        [SerializeField] private AnimationCurve wordCurve;
        public AnimationCurve words => wordCurve;
        [SerializeField] private float minFloatTextDuration;
        public float minDuration => minFloatTextDuration;
        
        private void Awake() {
            if (d != null && d != this)
                Destroy (this.gameObject);
            else
            {
                d = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
        /* Need to deal with dialogue box stuff now. Dialogue Engine needs to know:
         *  Who is speaking?
         *  What they are saying?
         *  Where should the text be sent to?
         *  What portraits should be used?
         *
         * Maybe individual "characters" should have their dialogue trees stored
         *  When player interacts, the other entity says to the Dialogue Engine:
         *      "Enter THIS tree, using THESE Sprites"
         *  What if we want a dialogue involving 3 people?
         */
    }
}

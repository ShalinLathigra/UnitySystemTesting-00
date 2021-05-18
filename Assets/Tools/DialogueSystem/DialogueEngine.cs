using UnityEngine;

namespace DialogueSystem
{
    public class DialogueEngine : MonoBehaviour
    {
        // Basically, each NPC will carry around a reference to their own dialogue tree.
        // If the player is near you, not holding another input, and 

        private DialogueTree _currentTree; 
        public static DialogueEngine d { get; private set; }

        
        private void Awake() {
            if (d != null && d != this)
                Destroy (this.gameObject);
            else
            {
                d = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}

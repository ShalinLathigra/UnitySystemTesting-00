using System;
using UnityEngine;

namespace DialogueSystem
{
    public class DialoguePlayer : MonoBehaviour
    {
        [SerializeField] private DialogueBranch head;
        //private bool _connectedToInput;

        //private void Awake()
        //{
        //    _connectedToInput = false;
        //}

        private void EnterDialogue(int _i)
        {
            // so basically, when the button is pressed && tree is not currently active
            if (_i != DialogueEngine.EnterKey) return;
            DialogueEngine.d.EnterDialogue(head);
        }
// That's it, DEngine should be responsible for TICKING THROUGH the dialogue tree TODO: THIS!!!!
//  Need alt UI for CREATING a dialogue tree
// Once Dialogue is entered, just click through pre-defined results until you reach the end. 

        private void OnTriggerEnter2D(Collider2D other)
        {
            //if (_connectedToInput) return;
            // If you've entered this space: Hook up to Input System
            InputManager.input.advanceDialogue += EnterDialogue;
            //_connectedToInput = true;
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            // If you've entered this space: Hook up to Input System
            //if (!_connectedToInput) return;
            
            InputManager.input.advanceDialogue -= EnterDialogue;
            DialogueEngine.d.ExitDialogue();
            //_connectedToInput = false;
        }
    }
}

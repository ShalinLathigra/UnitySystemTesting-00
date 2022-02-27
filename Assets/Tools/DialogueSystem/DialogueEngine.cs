using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Base;
using Easing;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DialogueSystem
{
    public class DialogueEngine : MonoBehaviour
    {
        // Basically, each NPC will carry around a reference to their own dialogue tree.
        // If the player is near you, not holding another input, and 

        private DialogueBranch _currentBranch;
        public static DialogueEngine d { get; private set; }

        public FloatingDialoguePreferences floatPrefs;

        [SerializeField] private RectTransform canvasTransform;
        private SimpleAnimation _simple;
        private Action<float> _callBack;
        private float _yPosition;
        
        public const int EnterKey = 0;

        private void Awake() {
            if (d != null && d != this)
                Destroy (gameObject);
            else
            {
                d = this;
                DontDestroyOnLoad(gameObject);
                
            }

            _currentBranch = null;
            
            _callBack = value => _yPosition = value;
            _simple = new SimpleAnimation(_callBack, timeScaled:false);
        }

        // Start Dialogue Chain
        public void EnterDialogue(DialogueBranch branch)
        {
            if (_currentBranch == branch) return;
            _currentBranch = branch;
            InputManager.input.exitDialogue += ExitDialogue;
            StartCoroutine(AnimateDialogueEntry(branch));
        }
        // Quit out of Dialogue Chain 
        public void ExitDialogue(int i = -1)
        {
            
            if (i != -1) return;
            StartCoroutine(AnimateDialogueExit());
            _currentBranch = null;
            // Need to create a coroutine which just interpolates the 
            /*
             1. Disable UI
             2. Animate Dialogue Exit
             3. Clear current tree data
             4. Resume game time
             */
        }

        // Begin Text overwrite
        private void SetDialogue(DialogueBranch branch)
        {
            
            // Animated Update the current UI Selections somehow
            // Re-start the text scrolls and everything
        }

        // Starts Entry coroutines
        private IEnumerator AnimateDialogueEntry(DialogueBranch branch)
        {
            InputManager.input.canMove = false;
            StartCoroutine(AnimateTimeScale(true));
            yield return new WaitUntil(() => _simple.complete);
            SetDialogue(branch);
            StartCoroutine(AnimateUIShift(true));
            yield return new WaitUntil(() => _simple.complete);
        }
        // Starts Exit Coroutines
        private IEnumerator AnimateDialogueExit()
        {
            StartCoroutine(AnimateUIShift(false));
            yield return new WaitUntil(() => _simple.complete);
            SetDialogue(null);
            StartCoroutine(AnimateTimeScale(false));
            yield return new WaitUntil(() => _simple.complete);
            
            InputManager.input.canMove = true;
            
        }

        // Controls UI translation up + down
        private IEnumerator AnimateUIShift(bool enter)
        {
            _simple = new SimpleAnimation(
                value => _yPosition = value, 
                canvasTransform.localPosition.y, 
                (enter) ? 0 : -360, 
                0.75f, 
                timeScaled: false
            );
            while (!_simple.complete)
            {
                _simple.Step();
                canvasTransform.localPosition = new Vector2(0.0f, _yPosition);
                yield return null;
            }
        }
        // Controls TimeScale transitions
        private IEnumerator AnimateTimeScale(bool enter)
        {
            _simple = new SimpleAnimation(
                value => Time.timeScale = value, 
                Time.timeScale,
                enter ? 0.5f : 1.0f, 
                0.25f, 
                timeScaled: false
            );
            while (!_simple.complete)
            {
                _simple.Step();
                yield return null;
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
         
         /*
         
         As per the note pictures (Assets/EntityAssets/Aseprite/DialogueSystemRough):
         
         A dialogue tree needs to store:
            List of all dialogue options
            List of transitions to an individual dialogue option
            List of transitions FROM ANY dialogue option
            Exit Dialogue button
            
            
            Each dialogue Branch should know:
                What picture to show
                What text to show
                What options to show
                    To what other branch each option leads
            
            Therefore Tree needs to know:
                Head / Where to start
                
                
                I don't think we need the Tree, the tree is just a Branch 
                
         
          */
    }
}

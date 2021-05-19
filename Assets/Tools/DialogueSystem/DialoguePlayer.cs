using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class DialoguePlayer : MonoBehaviour
    {
        private TMPTextScroll _scroll;
        [SerializeField] private List<DialogueObject> options;
        [SerializeField] private bool random;

        private void Awake()
        {
            _scroll = GetComponentInChildren<TMPTextScroll>();
        }
        private void DisplayText( int index )
        {
            if (index >= options.Count) return;
            _scroll.ResetAndPlay(options[index].text);
        }

        private void DisplayTextRandom( int index )
        {
            var i = Random.Range(0, 100) % options.Count;
            _scroll.ResetAndPlay(options[i].text);
        }

        private void DisplayText(string text) =>_scroll.ResetAndPlay(text);
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (random)
                InputManager.input.advanceDialogue += DisplayTextRandom;
            else
                InputManager.input.advanceDialogue += DisplayText;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (random)
                InputManager.input.advanceDialogue -= DisplayTextRandom;
            else
                InputManager.input.advanceDialogue -= DisplayText;
        }
    }
}

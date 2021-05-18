using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(menuName = "DialogueTree", fileName = "DialogueTree", order = 0)]
    public class DialogueTree : DialogueObject
    {
        public new static DialogueTree CreateInstance()
        {
            var tree = ScriptableObject.CreateInstance<DialogueTree>();
            tree.Init();
            return tree;
        }
        private new void Init()
        {
            base.Init();
            head = this;
        }
        public DialogueObject head;
        
        // <color=#RRGGBBAA></color>
        // http://digitalnativestudios.com/textmeshpro/docs/rich-text/
        // file:///D:/UnityProjects/PrototypeProject/Assets/TextMesh%20Pro/Documentation/TextMesh%20Pro%20User%20Guide%202016.pdf
        // Example 23 for how to animate vertex attributes. but how do I localize it?
        // Need to provide access to further sections through numbers? who knows. but this should be another aspect of the InputManager.
            
        /* TODO: Transition Input data out of PlayerInputWrapper, onto Engine.InputWrapper.
         *  TODO: Then need to remove InputWrapper references from the BehaviourTree/Branch 
         */
        
        /* What is the first thing I should do?
         * 1. Add dialogue input buttons to the InputWrapper
         * 2. add a trigger + Dialogue Processor gameObject to the window
         *      Trigger is entered, gameObject sees player input, Asks game Manager to transition from
         *      Engine to Dialogue Processor?
         *      Otherwise, if receiving a plain Dialogue bubble will write and scroll the corresponding text
         * 3. In dialogue mode, player input is killed, and only can skip through dialogue
         * 4. After dialogue completion, re-enable Engine, Kill Dialogue Processor
         */
    }
}
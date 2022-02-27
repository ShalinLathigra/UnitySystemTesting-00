using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(menuName = "PrototypeProject/Dialogue/DialogueBranch", fileName = "DialogueBranch", order = 0)]
    public class DialogueBranch : ScriptableObject
    {
        public static DialogueBranch CreateInstance()
        {
            var branch = ScriptableObject.CreateInstance<DialogueBranch>();
            branch.Init();
            return branch;
        }
        private void Init()
        {
            options = new List<BranchOption>();
        }

        [SerializeField]private Sprite speaker;
        public Sprite speakerLink => speaker;

        [SerializeField]private string text;
        public string textLink => text;

        [SerializeField] private List<BranchOption> options;
        public List<BranchOption> optionLink => options;
    }

    [Serializable]
    public struct BranchOption
    {
        public string text;
        public DialogueBranch branch;
    }
}
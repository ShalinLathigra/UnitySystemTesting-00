using System;
using System.Collections.Generic;
using Pixel;
using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(menuName = "PrototypeProject/Dialogue/FloatingDialogueObject", fileName = "FloatingDialogueObject", order = 2)]
    public sealed class FloatingDialogueObject : ScriptableObject
    {
        public static FloatingDialogueObject CreateInstance()
        {
            var obj = ScriptableObject.CreateInstance<FloatingDialogueObject>();
            obj.Init();
            return obj;
        }

        private void Init()
        {
            _text = "";
            _squashStretch = 0;
            _sheet = null;
        }
        [TextArea]
        [SerializeField] private string _text;
        [Range(-0.5f, 0.5f)]
        [SerializeField] private float _squashStretch;
        [SerializeField] private PixelSheet _sheet;

        public string text => _text;
        public float squashStretch => _squashStretch;
        public PixelSheet sheet => _sheet;
    }
}

using System;
using System.Collections.Generic;
using Pixel;
using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(menuName = "DialogueObject", fileName = "DialogueObject", order = 1)]
    public class DialogueObject : ScriptableObject
    {
        public static DialogueObject CreateInstance()
        {
            var obj = ScriptableObject.CreateInstance<DialogueObject>();
            obj.Init();
            return obj;
        }

        protected void Init()
        {
            _text = "";
            _squashStretch = 0;
            _sheet = null;
            children = new List<DialogueObject>();
        }
        [TextArea]
        [SerializeField] private string _text;
        [Range(-0.5f, 0.5f)]
        [SerializeField] private float _squashStretch;
        [SerializeField] private PixelSheet _sheet;

        public virtual string text => _text;
        public virtual float squashStretch => _squashStretch;
        public virtual PixelSheet sheet => _sheet;
        

        public List<DialogueObject> children;
    }
}

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
            text = "";
            squashStretch = 0;
            sheet = null;
            children = new List<DialogueObject>();
        }
        [TextArea]
        [SerializeField] private string text;
        [Range(-0.5f, 0.5f)]
        [SerializeField] private float squashStretch;
        [SerializeField] private PixelSheet sheet;
        
        public List<DialogueObject> children;
    }
}

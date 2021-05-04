using UnityEditor;
using UnityEngine;
using BStateMachine;

namespace Editor
{
    [CustomEditor(typeof(BehaviourStateMachine), true)]
    public class BehaviourStateMachineEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            // Need to create a label which displays:
            // State
            // State.DeepState
            var item = target as BehaviourStateMachine;
    
            EditorGUILayout.LabelField(item.GetStatePathString());
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            base.OnInspectorGUI();
        }
    }
}
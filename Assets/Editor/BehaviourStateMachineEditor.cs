using UnityEditor;
using UnityEngine;
using BehaviourStateTree;

namespace Editor
{
    [CustomEditor(typeof(StateTree), true)]
    public class BehaviourStateMachineEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            // Need to create a label which displays:
            // State
            // State.DeepState
            var item = target as StateTree;
    
            EditorGUILayout.LabelField(item.GetStatePathString());
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            base.OnInspectorGUI();
        }
    }
}
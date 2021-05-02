using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BehaviourStateMachine), true)]
public class BehaviourStateMachineEditor : Editor {
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
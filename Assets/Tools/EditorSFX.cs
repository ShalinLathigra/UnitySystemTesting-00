using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

// Sourced from: https://forum.unity.com/threads/way-to-play-audio-in-editor-using-an-editor-script.132042/ 
public static class EditorSFX
{
 
    public static void PlayClip(AudioClip clip, int startSample = 0, bool loop = false)
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
     
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod(
            "PlayPreviewClip",
            BindingFlags.Static | BindingFlags.Public,
            null,
            new Type[] { typeof(AudioClip), typeof(int), typeof(bool) },
            null
        );
 
        //Debug.Log(method);
        method.Invoke(
            null,
            new object[] { clip, startSample, loop }
        );
    }
 
    public static void StopAllClips()
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
 
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod(
            "StopAllPreviewClips",
            BindingFlags.Static | BindingFlags.Public,
            null,
            new Type[] { },
            null
        );
 
        //Debug.Log(method);
        method.Invoke(
            null,
            new object[] { }
        );
    }
}

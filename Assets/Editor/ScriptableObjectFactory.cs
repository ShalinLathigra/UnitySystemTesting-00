using DialogueSystem;
using Pixel;
using States;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace Editor
{
    internal class EndNameEdit : EndNameEditAction
    {
        public override void Action(int instanceID, string pathName, string resourceFile)
        {
            AssetDatabase.CreateAsset(EditorUtility.InstanceIDToObject(instanceID), AssetDatabase.GenerateUniqueAssetPath(pathName));
        }
    }
    public class ScriptableObjectFactory : EditorWindow
    {
        [MenuItem("Window/PrototypeProject/ObjectFactory")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ScriptableObjectFactory));
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.Label("State Data");
            if (GUILayout.Button("AirSO"))
            {
                AirSO asset = ScriptableObject.CreateInstance<AirSO>();
                ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                    asset.GetInstanceID(),
                    ScriptableObject.CreateInstance<EndNameEdit>(),
                    string.Format("{0}.asset", "AirSO"),
                    AssetPreview.GetMiniThumbnail(asset),
                    null
                    );
            }
            if (GUILayout.Button("MoveSO"))
            {
                MoveSO asset = ScriptableObject.CreateInstance<MoveSO>();
                ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                    asset.GetInstanceID(),
                    ScriptableObject.CreateInstance<EndNameEdit>(),
                    string.Format("{0}.asset", "MoveSO"),
                    AssetPreview.GetMiniThumbnail(asset),
                    null
                );
            }
            GUILayout.FlexibleSpace();
            GUILayout.Label("Pixel Objects");
            if (GUILayout.Button("PixelLibrary"))
            {
                PixelLibrary asset = PixelLibrary.CreateInstance();
                ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                    asset.GetInstanceID(),
                    ScriptableObject.CreateInstance<EndNameEdit>(),
                    string.Format("{0}.asset", "PixelLibrary"),
                    AssetPreview.GetMiniThumbnail(asset),
                    null
                );
            }
            if (GUILayout.Button("PixelSheet"))
            {
                PixelSheet asset = PixelSheet.CreateInstance();
                ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                    asset.GetInstanceID(),
                    ScriptableObject.CreateInstance<EndNameEdit>(),
                    string.Format("{0}.asset", "PixelSheet"),
                    AssetPreview.GetMiniThumbnail(asset),
                    null
                );
            }
            GUILayout.FlexibleSpace();
            GUILayout.Label("Dialogue System");
            if (GUILayout.Button("DialogueTree"))
            {
                var asset = DialogueTree.CreateInstance();
                ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                    asset.GetInstanceID(),
                    ScriptableObject.CreateInstance<EndNameEdit>(),
                    string.Format("{0}.asset", "DialogueTree"),
                    AssetPreview.GetMiniThumbnail(asset),
                    null
                );
            }
            if (GUILayout.Button("DialogueObject"))
            {
                var asset = DialogueObject.CreateInstance();
                ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                    asset.GetInstanceID(),
                    ScriptableObject.CreateInstance<EndNameEdit>(),
                    string.Format("{0}.asset", "DialogueObject"),
                    AssetPreview.GetMiniThumbnail(asset),
                    null
                );
            }
            GUILayout.FlexibleSpace();
            GUILayout.Label("Audio Toolkit");
            if (GUILayout.Button("AudioClipSO"))
            {
                AudioClipSO asset = ScriptableObject.CreateInstance<AudioClipSO>();
                ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                    asset.GetInstanceID(),
                    ScriptableObject.CreateInstance<EndNameEdit>(),
                    string.Format("{0}.asset", "AudioClipSO"),
                    AssetPreview.GetMiniThumbnail(asset),
                    null
                );
            }
            if (GUILayout.Button("AudioLibrarySO"))
            {
                AudioLibrarySO asset = ScriptableObject.CreateInstance<AudioLibrarySO>();
                ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                    asset.GetInstanceID(),
                    ScriptableObject.CreateInstance<EndNameEdit>(),
                    string.Format("{0}.asset", "AudioLibrarySO"),
                    AssetPreview.GetMiniThumbnail(asset),
                    null
                );
            }
            GUILayout.EndVertical();
        }
    }
}

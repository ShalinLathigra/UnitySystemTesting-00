using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
     * Summary: Editor used to create / mix audio clips in the Audio clip library.
     * Only stores one instance of list, accessible by objects with IAudioPlayer interface
     */ 
     
//TODO: AudioLibraryEditor bonus points for search functions / loading all clips in a folder
namespace Editor
{
    public class AudioLibraryEditor : EditorWindow
    {
        private const string BasePath = "Assets/ScriptableObjects/Audio/";

        [MenuItem ("Window/PrototypeProject/AudioLibrary")]
        public static void ShowWindow () {
            EditorWindow.GetWindow(typeof(AudioLibraryEditor));
        }

        public AudioLibrarySO audioLibraryData;

        void  OnEnable ()
        {
            if (!EditorPrefs.HasKey("AudioLibraryObjectPath")) return;
            string audioLibraryObjectPath = EditorPrefs.GetString("AudioLibraryObjectPath");
            audioLibraryData = AssetDatabase.LoadAssetAtPath (audioLibraryObjectPath, typeof(AudioLibrarySO)) as AudioLibrarySO;
            
            ResetNewAudioClipValues();
        }

        private void OnGUI() 
        {
            EditorGUIUtility.labelWidth = 75;
            DisplayHeader();

            if (isAudioLibraryDataNotNull)
            {
                DisplayCreateNewClip();
                DisplayAudioList();
            }
            if (GUI.changed) 
            {
                EditorUtility.SetDirty(audioLibraryData);
            }
        }

        private void DisplayAudioList()
        {
            GUILayout.Label ("AudioClipList", EditorStyles.boldLabel);
            GUILayout.BeginScrollView(Vector2.zero);
            // Here is where we'll go and display the whole existing list
            for (int i = 0; i < audioLibraryData.list.Count; i++)
            {
                DisplayAudioClipSo(audioLibraryData.list[i]);
            }
            GUILayout.EndScrollView();
        }

        private void DisplayAudioClipSo(AudioClipSO clip)
        {
            float spacing = 10;
            if (clip == null)
                return;
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();

            GUILayout.Space(20);
        
            // Row 0
            GUILayout.BeginHorizontal();
            clip.name = EditorGUILayout.TextField("Name:", clip.name);
            GUILayout.Space(spacing);
            clip.audioClip = (AudioClip)(EditorGUILayout.ObjectField("Audio Clip:", (Object)(clip.audioClip), typeof(AudioClip), false));
            GUILayout.EndHorizontal();
        
            // Row 1
            GUILayout.BeginHorizontal();
            clip.volume = EditorGUILayout.Slider("Volume:", clip.volume, 0, 1);
            GUILayout.Space(spacing);
            clip.pitch = EditorGUILayout.Slider("Pitch:", clip.pitch, -3, 3);
            GUILayout.EndHorizontal();

            // Row 2
            clip.loop = EditorGUILayout.Toggle("Loop:", clip.loop);

            GUILayout.Space(spacing);
            // Row 3
            if (GUILayout.Button("Play"))
                EditorSFX.PlayClip(clip.audioClip, 0, clip.loop);
            
            // Row 4
            if (GUILayout.Button("Stop"))
                EditorSFX.StopAllClips();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.EndVertical();

            //TODO: Replace this space with play / stop
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    
        private void DisplayHeader()
        {
            GUILayout.BeginHorizontal ();
            GUILayout.Label ("AudioLibrary Editor", EditorStyles.boldLabel);
            if (isAudioLibraryDataNotNull)
            {
                if (GUILayout.Button("Show AudioLibrary File")) 
                {
                    EditorUtility.FocusProjectWindow();
                    Selection.activeObject = audioLibraryData;
                }
            }
            if (GUILayout.Button("Open AudioLibrary")) 
            {
                OpenAudioLibrary();
            }
            if (GUILayout.Button("New AudioLibrary")) 
            {
                CreateNewAudioLibrary();
            
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = audioLibraryData;
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(20);
        }
    
        private Object _newClip;
        private string _newName;
        private float _newVolume = 1;
        private float _newPitch = 1;
        private bool _newLoop;
        private bool isAudioLibraryDataNotNull => audioLibraryData != null;

        private void DisplayCreateNewClip()
        {
            GUILayout.BeginHorizontal ();
            GUILayout.Label ("New AudioClipSO", EditorStyles.boldLabel);
            // if the required fields are all good: 
            GUILayout.BeginVertical ();
            _newName = EditorGUILayout.TextField("Name:", _newName);
            _newClip = EditorGUILayout.ObjectField("Audio Clip:", _newClip, typeof(AudioClip), false);
            _newVolume = EditorGUILayout.Slider("Volume:", _newVolume, 0, 1);
            _newPitch = EditorGUILayout.Slider("Pitch:", _newPitch, -3, 3);
            _newLoop = EditorGUILayout.Toggle("Loop:", _newLoop);
            GUILayout.EndVertical ();

            GUILayout.Space(40);

            if (GUILayout.Button("CreateClip") && _newClip != null) 
            {
                AddAudioClipSo();
                ResetNewAudioClipValues();
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(20);
        }

        void ResetNewAudioClipValues()
        {
            _newClip = null;
            _newName = "";
            _newVolume = 1;
            _newPitch = 1;
            _newLoop = false;
        }

        void AddAudioClipSo () 
        {
            //TODO: Check if name in library
            string clipName = (string.Equals(_newName, "")) ? _newClip.name : _newName;
            AudioClipSO newItem = CreateNewClip(clipName);
            newItem.init(_newClip as AudioClip, _newVolume, _newPitch, _newLoop);
            audioLibraryData.list.Add (newItem);
        }

        private static AudioClipSO CreateNewClip(string _clipName)
        {
            AudioClipSO asset = ScriptableObject.CreateInstance<AudioClipSO>();
            asset.name = _clipName;
            AssetDatabase.CreateAsset(asset, BasePath + $"AudioLibrary/{asset.name}.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            return asset;
        }

        void DeleteItem (int index) 
        {
            audioLibraryData.list.RemoveAt (index);
        }



        void OpenAudioLibrary () 
        {
            string absPath = EditorUtility.OpenFilePanel ("Select Inventory Item List", "", "");
            if (absPath.StartsWith(Application.dataPath)) 
            {
                string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
                audioLibraryData = AssetDatabase.LoadAssetAtPath (relPath, typeof(AudioLibrarySO)) as AudioLibrarySO;
                var audioClipSos = audioLibraryData?.list;
                if (audioClipSos == null)
                    audioClipSos = new List<AudioClipSO>();
                if (audioLibraryData) {
                    EditorPrefs.SetString("AudioLibraryObjectPath", relPath);
                }
            }
        }

        private void CreateNewAudioLibrary () 
        {
            //TODO: Add protection vs accidental overwrite
            audioLibraryData = CreateAudioLibraryData();
            if (audioLibraryData) 
            {
                audioLibraryData.list = new List<AudioClipSO>();
                string relPath = AssetDatabase.GetAssetPath(audioLibraryData);
                EditorPrefs.SetString("AudioLibraryObjectPath", relPath);
            }
        }


        private static AudioLibrarySO  CreateAudioLibraryData()
        {
            AudioLibrarySO asset = ScriptableObject.CreateInstance<AudioLibrarySO>();
            AssetDatabase.CreateAsset(asset, BasePath + "/AudioLibraryStorage.asset");
            AssetDatabase.SaveAssets();
            return asset;
        }
    
    }
}
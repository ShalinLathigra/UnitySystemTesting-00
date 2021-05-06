using System;
using Pixel;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace Editor
{
    public class PixelSheetEditor : EditorWindow
    {
        private const string SheetPath = "Assets/ScriptableObjects/PixelSheets";
        
        [MenuItem ("Window/PrototypeProject/PixelSheetEditor")]
        public static void ShowWindow () {
            GetWindow(typeof(PixelSheetEditor));
        }


        public PixelSheet targetSheet;
        public PixelFrame targetFrame;

        private string _newAnimPath;
        
        private void OnGUI()
        {
            EditorGUIUtility.labelWidth = 75;
            DisplayHeader();
            if (targetSheet != null)
            {
                DisplayActiveSheet();
            }
            DisplayCreateNewSheet();
        }

        private void DisplayHeader()
        {
            GUILayout.BeginHorizontal ();
            GUILayout.Label ("PixelSheet Editor", EditorStyles.boldLabel);
            if (targetSheet != null)
            {
                if (GUILayout.Button("Show Active Sheet")) 
                {
                    EditorUtility.FocusProjectWindow();
                    Selection.activeObject = targetSheet;
                }
            }
            if (GUILayout.Button("Open Sheet")) 
            {
                OpenPixelSheet();
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(20);
        }

        private void DisplayActiveSheet()
        {            
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            targetSheet.name = EditorGUILayout.TextField(targetSheet.name);// GUILayout.Label ($"{targetSheet.name}", EditorStyles.boldLabel);
            if (GUILayout.Button("New Frame"))
                CreatePixelFrame();
            if (targetFrame != null)
                if (GUILayout.Button("Delete Frame"))
                    DeleteFrame();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (targetSheet.frames.Count > 0)
            {
                if (GUILayout.Button("Next Frame"))
                    ChangeFrame(1);
                if (GUILayout.Button("Prev Frame"))
                    ChangeFrame(-1);   
            }
            GUILayout.EndHorizontal();
            targetSheet.frameRate = EditorGUILayout.Slider("FrameRate: ", targetSheet.frameRate, 0, 24);
            targetSheet.looping = EditorGUILayout.Toggle("Looping?: ", targetSheet.looping);
            if (targetFrame != null)
                DisplayActiveFrame();
            GUILayout.BeginScrollView(Vector2.zero);
            GUILayout.FlexibleSpace();

            for (int i = 0; i < targetSheet.frames.Count; i++)
            {
                GUILayout.Label ($"{i}: {targetSheet.frames[i].name}", EditorStyles.boldLabel);                
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
        }

        private void ChangeFrame(int dir)
        {
            int index = targetSheet.frames.IndexOf(targetFrame);
            index = (targetSheet.frames.Count + index + dir) % targetSheet.frames.Count;
            targetFrame = targetSheet.frames[index];
        }

        private void DeleteFrame()
        {
            // check if index % count is good, remove index from list
            if (!targetSheet.frames.Contains(targetFrame)) return;
            // Remove frame from sheet
            targetSheet.frames.Remove(targetFrame);
            //Remove frame from DB
            string assetPath = SheetPath + $"/{targetSheet.ownerDirectory}/{targetFrame.name}.asset";
            AssetDatabase.DeleteAsset(assetPath);
            // Clear targetFrame
            targetFrame = targetSheet.frames.Count > 0 ? targetSheet.currentFrame : null;
        }
        
        private void CreatePixelFrame () 
        {
            string newFrameName = targetSheet.name + "_" + targetSheet.frames.Count;
            PixelFrame newFrame = CreateNewPixelFrame(targetSheet, newFrameName);
            targetFrame = newFrame;
            targetSheet.frames.Add(targetFrame);
        }
        
        private static PixelFrame CreateNewPixelFrame(PixelSheet targetSheet, string name)
        {
            PixelFrame asset = ScriptableObject.CreateInstance<PixelFrame>();
            asset.name = name;
            
            asset.Init();
            
            string desiredPath = SheetPath + $"/{targetSheet.ownerDirectory}/{targetSheet.name}";
            AssetDatabase.CreateAsset(asset, desiredPath + $"/{asset.name}.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            return asset;
        }
/*
        private Object _newClip;
        private float _newVolume = 1;
        private float _newPitch = 1;
        private bool _newLoop;

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
            GUILayout.EndHorizontal();

            GUILayout.Space(20);
        }
        */
        bool hit, hurt;
        private void DisplayActiveFrame()
        {
            GUILayout.Space(20);
            GUILayout.BeginVertical();
            GUILayout.Label ($"{targetFrame.name}", EditorStyles.boldLabel);
            
            targetFrame.sprite = EditorGUILayout.ObjectField("Sprite: ", targetFrame.sprite, typeof(Sprite), false) as Sprite;
            
            GUILayout.BeginHorizontal();
            targetFrame.hitProps.active = EditorGUILayout.Toggle("Hit Box?", targetFrame.hitProps.active);
            GUILayout.BeginVertical();
            targetFrame.hitProps.shape.center =
                EditorGUILayout.Vector2Field("Center", targetFrame.hitProps.shape.center);
            targetFrame.hitProps.shape.size =
                EditorGUILayout.Vector2Field("Dims", targetFrame.hitProps.shape.size); 
            GUILayout.EndVertical();
            targetFrame.hurtProps.active = EditorGUILayout.Toggle("Hurt Box?", targetFrame.hurtProps.active);
            GUILayout.BeginVertical();
            targetFrame.hurtProps.shape.center =
                EditorGUILayout.Vector2Field("Center", targetFrame.hurtProps.shape.center);
            targetFrame.hurtProps.shape.size =
                EditorGUILayout.Vector2Field("Dims", targetFrame.hurtProps.shape.size);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            
            GUILayout.EndVertical();
            GUILayout.Space(20);
            GUILayout.FlexibleSpace();
            
            
            /* Need to display:
             * Sprite
             * 
             */ 
        }
        
        private void DisplayCreateNewSheet()
        {
            GUILayout.BeginHorizontal ();
            GUILayout.Label ("New Pixel Sheet", EditorStyles.boldLabel);
            // if the required fields are all good: 
            GUILayout.BeginVertical ();
            _newAnimPath = EditorGUILayout.TextField("Owner/Name", _newAnimPath);
            GUILayout.EndVertical ();

            GUILayout.Space(40);

            if (GUILayout.Button("Create New Pixel Sheet")) 
            {
                CreatePixelSheet();
                ResetNewSheetValues();
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(20);
        }

        void ResetNewSheetValues()
        {
            _newAnimPath = "/";
        }
        
        void OpenPixelSheet () 
        {
            string absPath = EditorUtility.OpenFilePanel ("Select Pixel Sheet", "", "");
            if (absPath.StartsWith(Application.dataPath)) 
            {
                string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
                targetSheet = AssetDatabase.LoadAssetAtPath (relPath, typeof(PixelSheet)) as PixelSheet;
                if (targetSheet) {
                    EditorPrefs.SetString("AudioLibraryObjectPath", relPath);
                    if (targetSheet.frames != null && targetSheet.frames.Count > 0)
                        targetFrame = targetSheet.frames[0];
                }
            }
        }

        private void CreatePixelSheet () 
        {
            if (string.Equals(_newAnimPath, "")) return;
            PixelSheet newItem = CreateNewPixelSheet(_newAnimPath);
            targetSheet = newItem;
            targetFrame = null;
        }
        
        private static PixelSheet CreateNewPixelSheet(string _animPath)
        {
            int i = _animPath.IndexOf("/", StringComparison.Ordinal);
            string owner = (i > -1) ? _animPath.Substring(0, i) : _animPath;
            string name = (i > -1) ? _animPath.Substring(i+1, _animPath.Length - i - 1) : _animPath;
            
            PixelSheet asset = ScriptableObject.CreateInstance<PixelSheet>();
            asset.name = name;

            string ownerPath = SheetPath + $"/{owner}";
            string assetPath = ownerPath + $"/{asset.name}";
            if (!AssetDatabase.IsValidFolder(ownerPath))
            {
                string guid = AssetDatabase.CreateFolder(SheetPath, $"{owner}");
                ownerPath = AssetDatabase.GUIDToAssetPath(guid);   
            }
            if (!AssetDatabase.IsValidFolder(assetPath))
            {
                string guid = AssetDatabase.CreateFolder(ownerPath, $"{asset.name}");
                assetPath = AssetDatabase.GUIDToAssetPath(guid);   
            }
            
            asset.Init(owner);
            
            AssetDatabase.CreateAsset(asset, assetPath + $"/{asset.name}.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            return asset;
        }
    }
}

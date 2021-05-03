using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

    /*
     * Summary: Editor used to create / mix audio clips in the Audio clip library.
     * Only stores one instance of list, accessible by objects with IAudioPlayer interface
     */ 
     
//TODO: AudioLibraryEditor bonus points for search functions / loading all clips in a folder
public class AudioLibraryEditor : EditorWindow
{
    private const string base_path = "Assets/ScriptableObjects/Audio/";

    [MenuItem ("Window/PrototypeProject/AudioLibrary")]
    public static void ShowWindow () {
        EditorWindow.GetWindow(typeof(AudioLibraryEditor));
    }

    public AudioLibrarySO audioLibraryData;

    void  OnEnable () {
        if(EditorPrefs.HasKey("AudioLibraryObjectPath")) 
        {
            string AudioLibraryObjectPath = EditorPrefs.GetString("AudioLibraryObjectPath");
            audioLibraryData = AssetDatabase.LoadAssetAtPath (AudioLibraryObjectPath, typeof(AudioLibrarySO)) as AudioLibrarySO;
            
            ResetNewAudioClipValues();
        }
    }

    private void OnGUI() 
    {
        EditorGUIUtility.labelWidth = 75;
        DisplayHeader();

        if (audioLibraryData != null)
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
            DisplayAudioClipSO(audioLibraryData.list[i]);
        }
        GUILayout.EndScrollView();
    }

    private void DisplayAudioClipSO(AudioClipSO clip)
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
        if (audioLibraryData != null)
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
    
    private Object newClip;
    private string newName;
    private float newVolume = 1;
    private float newPitch = 1;
    private bool newLoop;
    private void DisplayCreateNewClip()
    {
        GUILayout.BeginHorizontal ();
        GUILayout.Label ("New AudioClipSO", EditorStyles.boldLabel);
        // if the required fields are all good: 
        GUILayout.BeginVertical ();
        newName = EditorGUILayout.TextField("Name:", newName);
        newClip = EditorGUILayout.ObjectField("Audio Clip:", newClip, typeof(AudioClip), false);
        newVolume = EditorGUILayout.Slider("Volume:", newVolume, 0, 1);
        newPitch = EditorGUILayout.Slider("Pitch:", newPitch, -3, 3);
        newLoop = EditorGUILayout.Toggle("Loop:", newLoop);
        GUILayout.EndVertical ();

        GUILayout.Space(40);

        if (GUILayout.Button("CreateClip") && newClip != null) 
        {
            AddAudioClipSO();
            ResetNewAudioClipValues();
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(20);
    }

    void ResetNewAudioClipValues()
    {
        newClip = null;
        newName = "";
        newVolume = 1;
        newPitch = 1;
        newLoop = false;
    }

    void AddAudioClipSO () 
    {
        //TODO: Check if name in library
        string clipName = (string.Equals(newName, "")) ? newClip.name : newName;
        AudioClipSO newItem = CreateNewClip(clipName);
        newItem.init(newClip as AudioClip, newVolume, newPitch, newLoop);
        audioLibraryData.list.Add (newItem);
    }
    public static AudioClipSO CreateNewClip(string _clipName)
    {
        AudioClipSO asset = ScriptableObject.CreateInstance<AudioClipSO>();
        asset.name = _clipName;
        AssetDatabase.CreateAsset(asset, base_path + string.Format("AudioLibrary/{0}.asset", asset.name));
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
            if (audioLibraryData.list == null)
                audioLibraryData.list = new List<AudioClipSO>();
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
    
    
    public static AudioLibrarySO  CreateAudioLibraryData()
    {
        AudioLibrarySO asset = ScriptableObject.CreateInstance<AudioLibrarySO>();
        AssetDatabase.CreateAsset(asset, base_path + "/AudioLibraryStorage.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
    
}

/*

    void  OnGUI () {
        GUILayout.BeginHorizontal ();
        GUILayout.Label ("Inventory Item Editor", EditorStyles.boldLabel);
        if (inventoryItemList != null) {
            if (GUILayout.Button("Show Item List")) 
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = inventoryItemList;
            }
        }
        if (GUILayout.Button("Open Item List")) 
        {
                OpenItemList();
        }
        if (GUILayout.Button("New Item List")) 
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = inventoryItemList;
        }
        GUILayout.EndHorizontal ();

        if (inventoryItemList == null) 
        {
            GUILayout.BeginHorizontal ();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Item List", GUILayout.ExpandWidth(false))) 
            {
                CreateNewItemList();
            }
            if (GUILayout.Button("Open Existing Item List", GUILayout.ExpandWidth(false))) 
            {
                OpenItemList();
            }
            GUILayout.EndHorizontal ();
        }

            GUILayout.Space(20);

        if (inventoryItemList != null) 
        {
            GUILayout.BeginHorizontal ();

            GUILayout.Space(10);

            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false))) 
            {
                if (viewIndex > 1)
                    viewIndex --;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false))) 
            {
                if (viewIndex < inventoryItemList.itemList.Count) 
                {
                    viewIndex ++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Item", GUILayout.ExpandWidth(false))) 
            {
                AddItem();
            }
            if (GUILayout.Button("Delete Item", GUILayout.ExpandWidth(false))) 
            {
                DeleteItem(viewIndex - 1);
            }

            GUILayout.EndHorizontal ();
            if (inventoryItemList.itemList == null)
                Debug.Log("Inventory is empty");
            if (inventoryItemList.itemList.Count > 0) 
            {
                GUILayout.BeginHorizontal ();
                viewIndex = Mathf.Clamp (EditorGUILayout.IntField ("Current Item", viewIndex, GUILayout.ExpandWidth(false)), 1, inventoryItemList.itemList.Count);
                / / Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count);
                EditorGUILayout.LabelField ("of   " +  inventoryItemList.itemList.Count.ToString() + "  items", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal ();

                inventoryItemList.itemList[viewIndex-1].itemName = EditorGUILayout.TextField ("Item Name", inventoryItemList.itemList[viewIndex-1].itemName as string);
                inventoryItemList.itemList[viewIndex-1].itemIcon = EditorGUILayout.ObjectField ("Item Icon", inventoryItemList.itemList[viewIndex-1].itemIcon, typeof (Texture2D), false) as Texture2D;
                inventoryItemList.itemList[viewIndex-1].itemObject = EditorGUILayout.ObjectField ("Item Object", inventoryItemList.itemList[viewIndex-1].itemObject, typeof (Rigidbody), false) as Rigidbody;

                GUILayout.Space(10);

                GUILayout.BeginHorizontal ();
                inventoryItemList.itemList[viewIndex-1].isUnique = (bool)EditorGUILayout.Toggle("Unique", inventoryItemList.itemList[viewIndex-1].isUnique, GUILayout.ExpandWidth(false));
                inventoryItemList.itemList[viewIndex-1].isIndestructible = (bool)EditorGUILayout.Toggle("Indestructable", inventoryItemList.itemList[viewIndex-1].isIndestructible,  GUILayout.ExpandWidth(false));
                inventoryItemList.itemList[viewIndex-1].isQuestItem = (bool)EditorGUILayout.Toggle("QuestItem", inventoryItemList.itemList[viewIndex-1].isQuestItem,  GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal ();

                GUILayout.Space(10);

                GUILayout.BeginHorizontal ();
                inventoryItemList.itemList[viewIndex-1].isStackable = (bool)EditorGUILayout.Toggle("Stackable ", inventoryItemList.itemList[viewIndex-1].isStackable , GUILayout.ExpandWidth(false));
                inventoryItemList.itemList[viewIndex-1].destroyOnUse = (bool)EditorGUILayout.Toggle("Destroy On Use", inventoryItemList.itemList[viewIndex-1].destroyOnUse,  GUILayout.ExpandWidth(false));
                inventoryItemList.itemList[viewIndex-1].encumbranceValue = EditorGUILayout.FloatField("Encumberance", inventoryItemList.itemList[viewIndex-1].encumbranceValue,  GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal ();

                GUILayout.Space(10);

            } 
            else 
            {
                GUILayout.Label ("This Inventory List is Empty.");
            }
        }
        if (GUI.changed) 
        {
            EditorUtility.SetDirty(inventoryItemList);
        }
    }

    void OpenItemList () 
    {
        string absPath = EditorUtility.OpenFilePanel ("Select Inventory Item List", "", "");
        if (absPath.StartsWith(Application.dataPath)) 
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            inventoryItemList = AssetDatabase.LoadAssetAtPath (relPath, typeof(InventoryItemList)) as InventoryItemList;
            if (inventoryItemList.itemList == null)
                inventoryItemList.itemList = new List<InventoryItem>();
            if (inventoryItemList) {
                EditorPrefs.SetString("AudioLibraryObjectPath", relPath);
            }
        }
    }

    void AddItem () 
    {
        InventoryItem newItem = new InventoryItem();
        newItem.itemName = "New Item";
        inventoryItemList.itemList.Add (newItem);
        viewIndex = inventoryItemList.itemList.Count;
    }

    void DeleteItem (int index) 
    {
        inventoryItemList.itemList.RemoveAt (index);
    }
}
*/
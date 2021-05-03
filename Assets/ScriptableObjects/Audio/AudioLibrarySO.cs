using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioLibrarySO : ScriptableObject 
{
    [SerializeField]
    public List<AudioClipSO> list;
}
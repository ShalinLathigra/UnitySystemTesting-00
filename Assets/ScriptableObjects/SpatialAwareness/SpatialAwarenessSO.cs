using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpatialAwarenessSO", menuName = "PrototypeProject/CharacterSO/SpatialAwarenessSO")]
public class SpatialAwarenessSO : ScriptableObject 
{
    [Range(3, 9)]
    public int numRaysX;
    public int numRaysY;
    public float skinWidth;
    public float hitDistance;
    public float maxGroundDistance;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpatialAwarenessSO", menuName = "PrototypeProject/SpatialAwarenessSO")]
public class SpatialAwarenessSO : ScriptableObject 
{
    public int numRaysX;
    public int numRaysY;
    public float skinWidth;
    public float hitDistance;
}
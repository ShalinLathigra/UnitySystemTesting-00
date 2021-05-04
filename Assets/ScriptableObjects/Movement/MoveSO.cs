using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveSO", menuName = "PrototypeProject/CharacterSO/MoveSO")]
public class MoveSO : ScriptableObject 
{
    public AnimationCurve accelCurve; 
    public float maxSpeed;
}
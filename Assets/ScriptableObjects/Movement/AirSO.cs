using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "AirSO", menuName = "PrototypeProject/CharacterSO/AirSO")]
public class AirSO : ScriptableObject 
{
    public AnimationCurve jumpCurve; 
    public AnimationCurve longFallCurve; 
    public AnimationCurve shortFallCurve;
    public float maxAirSpeed;
    public float maxJumpSpeed;
    public float moveTransition;
}
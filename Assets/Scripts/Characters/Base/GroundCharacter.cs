using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroundCharacter : Character, IMoveEntity
{
    [SerializeField] private MoveSO _moveSO;
    public MoveSO moveSO { get{ return _moveSO; } }
}

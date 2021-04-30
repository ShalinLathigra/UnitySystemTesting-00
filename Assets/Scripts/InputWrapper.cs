using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputWrapper : MonoBehaviour, IInputWrapper
{
    public virtual bool shouldMove { get; }
    public virtual bool shouldJump { get; }
    public virtual Vector2 directionalInput { get; }
    public virtual float horizontalInput { get; }
    public virtual float verticalInput { get; }
    
}

public interface IInputWrapper
{
    bool shouldMove { get; }
    bool shouldJump { get; }
    Vector2 directionalInput { get; }
    float horizontalInput { get; }
    float verticalInput { get; }
}
using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using Pixel;
using UnityEngine;

public class Engine : MonoBehaviour
{
    private static Engine _e;
    public static Engine e => _e;

    public Player player;

    public float maxFallSpeed = -50.0f;

    private void Awake() {
        if (_e != null && _e != this)
            Destroy (this.gameObject);
        else
            _e = this;
    }
    
    public void ResolveCollision(PixelBox origin, PixelBox target)
    {
        Debug.Log(origin.name + " " + target.name);
    }

    public void RegisterPixelAnimator(PixelBoxAnimator _pixelBoxAnimator)
    {
        _pixelBoxAnimator.hitCollision += ResolveCollision;
    }

    public void DeRegisterPixelAnimator(PixelBoxAnimator _pixelBoxAnimator)
    {
        _pixelBoxAnimator.hitCollision -= ResolveCollision;
    }
}

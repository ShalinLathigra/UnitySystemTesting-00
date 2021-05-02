using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public static Engine _e;
    public static Engine e { get { return _e; } }

    public Player player;

    public float maxFallSpeed = -100.0f;

    private void Awake() {
        if (_e != null && _e != this)
            Destroy (this.gameObject);
        else
            _e = this;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPTestColorScroll : MonoBehaviour
{
    private TextMeshPro _tmp;
    // Start is called before the first frame update
    private float _lastChar;
    [SerializeField] private float toNext = 0.25f;

    private void Start()
    {
        _tmp = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
        Reset();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time < _lastChar + toNext) return;
        _tmp.maxVisibleCharacters += 1;
        _lastChar = Time.time;
    }

    public void Reset()
    {
        _tmp.maxVisibleCharacters = 0;  // Works
        _lastChar = 0;
    }
    public void ResetAndPlay(string str)
    {
        _tmp.text = str;
        _tmp.maxVisibleCharacters = 0;  // Works
        _lastChar = 0;
    }
}

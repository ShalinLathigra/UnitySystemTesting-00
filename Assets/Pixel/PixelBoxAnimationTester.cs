using System;
using System.Collections;
using System.Collections.Generic;
using Pixel;
using UnityEngine;

public class PixelBoxAnimationTester : MonoBehaviour
{
    public PixelBoxAnimator anim;
    public List<PixelSheet> aList;
    public float pause;
    private float last;

    private int i = 0;
    private void Start()
    {
        last = Time.time;
        anim.pixelComplete += Woop;
    }

    private void Update()
    {
        if ((Time.time - last) > pause)
        {
            anim.Play(aList[i]);
            i = (i + 1) % aList.Count;
            last = Time.time;
        }
    }

    private void Woop()
    {
        Debug.Log("Woop!");
    }
}

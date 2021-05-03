using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipPlayer : MonoBehaviour
{
    private AudioSource source;

    private void Awake() {
        source = GetComponent<AudioSource>();
        if (source == null)
            source = gameObject.AddComponent<AudioSource>();
    }

    public void Play(AudioClipSO clip)
    {
        if (source != null && clip != null)
        {
            source.Stop();
            source.volume = clip.volume;
            source.pitch = clip.pitch;
            if (clip.loop)
            {
                source.clip = clip.audioClip;
                source.Play();
            }
            else
            {
                source.PlayOneShot(clip.audioClip);
            }
        }
    }
    // What is this responsible for?
    // It is provided a clip, contains some number of maximum clips playing. 
    // on startup, create an empty child, attach several audio components to it
}

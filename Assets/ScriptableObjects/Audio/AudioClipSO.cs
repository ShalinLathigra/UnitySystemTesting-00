using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "AudioClipSO", menuName = "PrototypeProject/AudioClipSO", order = 0)]
public class AudioClipSO : ScriptableObject 
{
    public void init(AudioClip _audioClip, float _volume, float _pitch, bool _loop)
    {
        audioClip = _audioClip;
        volume = Mathf.Clamp(_volume, 0, 1);
        pitch = Mathf.Clamp(_pitch, -3, 3);
        loop = _loop;
    }

    public AudioClip audioClip;

    [Range(0, 1)]
    public float volume = 1.0f;

    [Range(-3, 3)]
    public float pitch = 1.0f;
    public bool loop;
}
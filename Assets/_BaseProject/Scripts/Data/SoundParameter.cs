using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundParameter : ScriptableObject
{
    [SerializeField]
    public SoundVolume Volume;
    public AudioClip[] BGMClip;
    public AudioClip[] SEClip;
    public AudioClip[] VoiceClip;
}

[System.Serializable]
public class SoundVolume
{
    [Range(0, 1)]
    public float BGM = 1.0f;
    [Range(0, 1)]
    public float SE = 1.0f;
    [Range(0, 1)]
    public float Voice = 1.0f;
    public bool Mute = false;
}
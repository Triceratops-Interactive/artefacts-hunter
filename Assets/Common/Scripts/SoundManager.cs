using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    
    private AudioSource _backgroundMusicSource;
    private AudioSource _soundEffectSource;

    private void Awake()
    {
        if (instance != null)
        {
            throw new Exception("Tried to instantiate SoundManager multiple times!");
        }

        instance = this;
        
        _backgroundMusicSource = GameObject.Find("Background Music Player").GetComponent<AudioSource>();
        _soundEffectSource = GameObject.Find("Sound Effect Player").GetComponent<AudioSource>();
    }

    private void Start()
    {
        ToggleMute();
    }

    public void ToggleMute()
    {
        _soundEffectSource.mute = GameState.instance.mute;
        _backgroundMusicSource.mute = GameState.instance.mute;
    }

    public AudioSource GetMusicSource()
    {
        return _backgroundMusicSource;
    }

    public AudioSource GetEffectSource()
    {
        return _soundEffectSource;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{ 
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds,  (sound) => sound.name == name);
        if (!s.source.isPlaying)
        {
            s.source.Play();
        }
    } 

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, (sound) => sound.name == name);
        if (s.source.isPlaying)
        {
            s.source.Stop();
        }
    }

    public bool IsCurrentlyPlaying(string name)
    {
        Sound sound = new Sound();
        sound.clip = FindObjectOfType<AudioSource>().clip;
        if(sound.clip)
        {
            return true;
        } else
        {
            return false;
        }
    }
}

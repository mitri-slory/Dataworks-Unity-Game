using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    void Start()
    {
        if (!NoTheme)
        {
            Play("Theme");
        }
    }

    public bool KeepIntoNextScene;
    public AudioMixerGroup audioMixer;

    public bool NoTheme;
    public AudioSound[] Sounds;
    public static AudioManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if (KeepIntoNextScene)
            DontDestroyOnLoad(gameObject);

        foreach(AudioSound s in Sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.outputAudioMixerGroup = audioMixer;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
        }
    }

    public void Play (string name)
    {
        AudioSound s = Array.Find(Sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("The Audioclip: " + name + " could not be found. Check for errors in the AudioManager component and it's code.");
            return;
        }
        s.Source.Play();
    }

    public void Stop(string name)
    {
        AudioSound s = Array.Find(Sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("The Audioclip: " + name + " could not be found. Check for errors in the AudioManager component and it's code.");
            return;
        }
        s.Source.Stop();
    }


    public void FadeOutVolume(string name)
    {
        AudioSound s = Array.Find(Sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("The Audioclip: " + name + " could not be found. Check for errors in the AudioManager component and it's code.");
            return;
        }

        s.Source.volume = 0.4f;
        s.Source.volume = 0.3f;
        s.Source.volume = 0.2f;
        s.Source.volume = 0.1f;
    }
}

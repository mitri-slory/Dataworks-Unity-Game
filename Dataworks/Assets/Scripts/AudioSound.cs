﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioSound
{
    public string name;

    public AudioClip Clip;

    [Range(0f,1f)]
    public float Volume;

    [Range(0f,3f)]
    public float Pitch;

    public bool Loop;


    [HideInInspector]
    public AudioSource Source;



}

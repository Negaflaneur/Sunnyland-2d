﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

  public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetScreenSize(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}

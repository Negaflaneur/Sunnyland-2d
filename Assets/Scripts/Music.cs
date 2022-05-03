using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource music;
    public static Music musicInstance;
    void Awake()
    {
        if (!musicInstance)
        {
            musicInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
            music.Play();
    }
}

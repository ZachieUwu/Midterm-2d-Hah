using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    public AudioClip background;
    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
}

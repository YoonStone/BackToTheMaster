using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour {

    public AudioClip  clips;
    AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void SoundPlay()
    {
        _audio.clip = clips;
        _audio.Play();
    }

    public void SoundStop()
    {
        _audio.Stop();
    }

}

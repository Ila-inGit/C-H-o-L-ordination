using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private static SoundManager instance;

    private AudioSource audioSource;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<SoundManager>();
            return instance;
        }
    }

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Playsound(AudioClip audioToPlay)
    {
        audioSource.PlayOneShot(audioToPlay);
    }
    public void PutOnLoop(AudioClip audioToPlay)
    {
        audioSource.loop = true;
        audioSource.clip = audioToPlay;
        audioSource.Play();
    }
    public void StopLoop()
    {
        audioSource.Stop();
        audioSource.clip = null;
        audioSource.loop = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private static SoundManager instance;

    [SerializeField]
    public AudioSource audioSource;

    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null) instance = GameObject.FindObjectOfType<SoundManager>();
    }

    public void Playsound(AudioClip audioToPlay)
    {
        // audioSource.PlayOneShot(audioToPlay);
        audioSource.clip = audioToPlay;
        audioSource.Play();
    }
    public void PutOnLoop(AudioClip audioToPlay)
    {
        audioSource.clip = null;
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

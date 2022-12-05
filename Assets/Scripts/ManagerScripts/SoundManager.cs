using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private static SoundManager instance;
    public AudioSource audioSource;
    [HideInInspector]
    public AudioClip audioToPlay;
    public bool canPlay { get; set; }


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
        if (audioSource == null) audioSource = gameObject.GetComponent<AudioSource>();
        canPlay = false;
        StartCoroutine(playSoundCorutine());
    }

    public void CanPlay(AudioClip clip)
    {
        audioToPlay = clip;
        canPlay = true;
    }

    IEnumerator playSoundCorutine()
    {
        while (!canPlay)
        {
            yield return null;
        }
        if (audioToPlay != null)
        {
            audioSource.clip = audioToPlay;
            audioSource.Play();
            yield return new WaitForSeconds(audioToPlay.length);
            canPlay = false;
        }
        StartCoroutine(playSoundCorutine());
    }

    public void Playsound(AudioClip clip)
    {
        if (audioSource == null) audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PutOnLoop(AudioClip audioToPlay)
    {
        if (audioSource == null) audioSource = gameObject.GetComponent<AudioSource>();
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

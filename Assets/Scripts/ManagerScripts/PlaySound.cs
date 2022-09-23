using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private bool isPlayed = false;

    [HideInInspector] public bool canStart = false;

    public PlaySound nextPlaySound;

    public void PlayAudioClip(AudioClip audioToPlay)
    {
        if (!isPlayed && canStart)
        {
            SoundManager.Instance.Playsound(audioToPlay);
        }
    }

    public void SetIsPlayed()
    {
        isPlayed = true;
        if (nextPlaySound != null)
            nextPlaySound.canStart = true;
    }

}

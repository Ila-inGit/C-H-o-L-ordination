using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public void PlayAudioClip(AudioClip audioToPlay)
    {
        SoundManager.Instance.Playsound(audioToPlay);
    }
}

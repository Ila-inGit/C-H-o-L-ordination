using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class FigureEightMovement : MonoBehaviour
{
    public Transform planetTransform;
    public AudioClip easyRhythm;
    public AudioClip mediumRhythm;
    public AudioClip difficultRhythm;
    public AudioClip easyMusic;
    public AudioClip mediumMusic;
    public AudioClip difficultMusic;
    private bool canMove = false;
    private Vector3 initpos;

    private Difficulty difficulty;

    private float speed = 0.0f;

    private float _x, _y, _deltaSpace, _scale;

    private float A = 0.75f;
    private float B = 0.5f;

    private void Awake()
    {
        initpos = new Vector3(
            planetTransform.localPosition.x - 0.75f,
            planetTransform.localPosition.y,
            planetTransform.localPosition.z
        );

        if (SceneChangerManager.Instance != null)
        {
            difficulty = SceneChangerManager.Instance.getDifficulty();
            if (difficulty == Difficulty.EASY)
            {
                speed = 0.3926f;
            }
            else if (difficulty == Difficulty.MEDIUM)
            {
                speed = 0.5236f;
            }
            else
            {
                speed = 0.6545f;
            }
        }
        else
        {
            speed = 0.5236f;
        }

        StartSound();
    }

    private void StartSound()
    {
        if (difficulty == Difficulty.EASY)
        {
            if (SceneChangerManager.Instance.isMusicSynch() && easyMusic != null)
                SoundManager.Instance.PutOnLoop(easyMusic);
            if (SceneChangerManager.Instance.isRhythmSynch() && easyRhythm != null)
                SoundManager.Instance.PutOnLoop(easyRhythm);
            if (SceneChangerManager.Instance.isMusicNotSynch() && mediumMusic != null)
                SoundManager.Instance.PutOnLoop(mediumMusic);
            if (SceneChangerManager.Instance.isRhythmNotSynch() && mediumRhythm != null)
                SoundManager.Instance.PutOnLoop(mediumRhythm);
            StartCoroutine(Wait(1f));
        }
        else if (difficulty == Difficulty.MEDIUM)
        {
            if (SceneChangerManager.Instance.isMusicSynch() && mediumMusic != null)
                SoundManager.Instance.PutOnLoop(mediumMusic);
            if (SceneChangerManager.Instance.isRhythmSynch() && mediumRhythm != null)
                SoundManager.Instance.PutOnLoop(mediumRhythm);
            if (SceneChangerManager.Instance.isMusicNotSynch() && difficultMusic != null)
                SoundManager.Instance.PutOnLoop(difficultMusic);
            if (SceneChangerManager.Instance.isRhythmNotSynch() && difficultRhythm != null)
                SoundManager.Instance.PutOnLoop(difficultRhythm);
            StartCoroutine(Wait(0.9f));
        }
        else if (difficulty == Difficulty.DIFFICULT)
        {
            if (SceneChangerManager.Instance.isMusicSynch() && difficultMusic != null)
                SoundManager.Instance.PutOnLoop(difficultMusic);
            if (SceneChangerManager.Instance.isRhythmSynch() && difficultRhythm != null)
                SoundManager.Instance.PutOnLoop(difficultRhythm);
            if (SceneChangerManager.Instance.isMusicNotSynch() && mediumMusic != null)
                SoundManager.Instance.PutOnLoop(mediumMusic);
            if (SceneChangerManager.Instance.isRhythmNotSynch() && mediumRhythm != null)
                SoundManager.Instance.PutOnLoop(mediumRhythm);
            StartCoroutine(Wait(0.8f));
        }

    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            _deltaSpace += Time.deltaTime * speed;
            _x = A * (Mathf.Cos(_deltaSpace));
            _y = B * (Mathf.Sin(2 * _deltaSpace) / 2);
            planetTransform.localPosition =
                new Vector3(initpos.x + _x, initpos.y + _y, initpos.z);


            //if we want to restrict the area we have increment the value of Cos
            if (Mathf.Cos(_deltaSpace) >= Mathf.Cos(5.8f))
            {
                gameObject.GetComponent<Interactable>().enabled = true;
                gameObject.GetComponent<PressableButtonHoloLens2>().enabled = true;
                if (FindObjectOfType<TouchesCounter>() != null && FindObjectOfType<TouchesCounter>().isInsideAngle == false)
                    FindObjectOfType<TouchesCounter>().SetIsInsideAngle(true, Constants.RIGHT_ANGLE);

                // Debug.Log("bottom box: "+_rightCounter + "touches");
            }
            else if (Mathf.Cos(_deltaSpace) <= -Mathf.Cos(5.8f))
            {
                gameObject.GetComponent<Interactable>().enabled = true;
                gameObject.GetComponent<PressableButtonHoloLens2>().enabled = true;

                // Debug.Log("bottom box: "+_leftCounter + "touches");

                if (FindObjectOfType<TouchesCounter>() != null && FindObjectOfType<TouchesCounter>().isInsideAngle == false)
                    FindObjectOfType<TouchesCounter>().SetIsInsideAngle(true, Constants.LEFT_ANGLE);
            }
            else
            {
                gameObject.GetComponent<Interactable>().enabled = false;
                gameObject.GetComponent<PressableButtonHoloLens2>().enabled = false;
                // Debug.Log("not near box");

                if (FindObjectOfType<TouchesCounter>() != null && FindObjectOfType<TouchesCounter>().isInsideAngle == true)
                    FindObjectOfType<TouchesCounter>().SetIsInsideAngle(false, Constants.ANGLE);
            }
        }

    }
}

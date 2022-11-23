using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class FigureEightMovement : MonoBehaviour
{
    public Transform planetTransform;
    [SerializeField]
    AudioClip[] audioClip;
    private bool isPlayed = false;
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
            if (difficulty == Difficulty.easy)
            {
                speed = 0.3926f;
            }
            else if (difficulty == Difficulty.medium)
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


    }

    void FixedUpdate()
    {
        if (!isPlayed)
        {
            isPlayed = true;
            if (difficulty == Difficulty.easy)
            {
                SoundManager.Instance.PutOnLoop(audioClip[0]);
            }
            else if (difficulty == Difficulty.medium)
            {
                SoundManager.Instance.PutOnLoop(audioClip[1]);
            }
            else
            {
                SoundManager.Instance.PutOnLoop(audioClip[2]);
            }
        }
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

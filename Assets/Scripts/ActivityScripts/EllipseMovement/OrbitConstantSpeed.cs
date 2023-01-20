using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using System.Collections;

public class OrbitConstantSpeed : MonoBehaviour
{

    public Transform planetTransform;
    public AudioClip easyRhythm;
    public AudioClip mediumRhythm;
    public AudioClip difficultRhythm;
    public AudioClip easyMusic;
    public AudioClip mediumMusic;
    public AudioClip difficultMusic;
    private bool canMove = true;
    [SerializeField]
    private float speed;
    private Difficulty difficulty;

    private const float A = 0.75f;
    private const float B = 0.5f;
    private float _x, _y, _deltaSpace;
    private Vector3 initpos;
    private void Awake()
    {
        initpos = new Vector3(
            planetTransform.localPosition.x,
            planetTransform.localPosition.y - 0.5f,
            planetTransform.localPosition.z);

        if (SceneChangerManager.Instance != null)
        {
            difficulty = SceneChangerManager.Instance.getDifficulty();
            if (difficulty == Difficulty.EASY)
            {
                speed = 0.5236f;
            }
            else if (difficulty == Difficulty.MEDIUM)
            {
                speed = 0.6545f;
            }
            else
            {
                speed = 0.5236f;
            }
        }
        else
        {
            speed = 0.6545f;
        }
        //StartSound();
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
            StartCoroutine(Wait(0.6f));
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
            StartCoroutine(Wait(0.5f));
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
            StartCoroutine(Wait(0.4f));
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
            // the two values can be changed to make the trajectory change
            //constant speed
            _deltaSpace += Time.deltaTime * speed; // beta
            // Debug.Log(_deltaSpace);
            _x = A * Mathf.Sin(_deltaSpace);
            _y = B * Mathf.Cos(_deltaSpace);
            planetTransform.localPosition = new Vector3(initpos.x + _x, initpos.y + _y, initpos.z);

            //if we want to restrict the area we have increment the value of Sin
            if (Mathf.Cos(_deltaSpace) <= Mathf.Sin(-0.85f))
            {
                gameObject.GetComponent<Interactable>().enabled = true;
                gameObject.GetComponent<PressableButtonHoloLens2>().enabled = true;

                if (FindObjectOfType<TouchesCounter>() != null && FindObjectOfType<TouchesCounter>().isInsideAngle == false)
                    FindObjectOfType<TouchesCounter>().SetIsInsideAngle(true, Constants.BOTTOM_ANGLE);
            }
            else if (Mathf.Cos(_deltaSpace) >= Mathf.Sin(0.85f))
            {
                gameObject.GetComponent<Interactable>().enabled = true;
                gameObject.GetComponent<PressableButtonHoloLens2>().enabled = true;

                if (FindObjectOfType<TouchesCounter>() != null && FindObjectOfType<TouchesCounter>().isInsideAngle == false)
                    FindObjectOfType<TouchesCounter>().SetIsInsideAngle(true, Constants.TOP_ANGLE);
            }
            else
            {
                gameObject.GetComponent<Interactable>().enabled = false;
                gameObject.GetComponent<PressableButtonHoloLens2>().enabled = false;
                if (FindObjectOfType<TouchesCounter>() != null && FindObjectOfType<TouchesCounter>().isInsideAngle == true)
                    FindObjectOfType<TouchesCounter>().SetIsInsideAngle(false, Constants.ANGLE);
            }

        }
    }
}
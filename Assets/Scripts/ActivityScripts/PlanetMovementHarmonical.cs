using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using System.Collections;


public class PlanetMovementHarmonical : MonoBehaviour
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
    private float _deltaSpace;
    private float speed;


    private void Awake()
    {
        initpos = new Vector3(
            planetTransform.localPosition.x - 0.75f,
            planetTransform.localPosition.y,
            planetTransform.localPosition.z);

        if (SceneChangerManager.Instance != null)
        {
            difficulty = SceneChangerManager.Instance.getDifficulty();
            if (difficulty == Difficulty.EASY)
            {
                speed = 0.589f;
            }
            else if (difficulty == Difficulty.MEDIUM)
            {
                speed = 0.72f;
            }
            else
            {
                speed = 0.851f;
            }
        }
        else
        {
            speed = 0.72f;
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
            // the two values can be changed to make the trajectory change
            _deltaSpace += Time.deltaTime * speed;
            float x = 0.75f * Mathf.Cos(_deltaSpace);

            if (Mathf.Cos(_deltaSpace) <= -Mathf.Cos(5.5f))
            {
                gameObject.GetComponent<Interactable>().enabled = true;
                gameObject.GetComponent<PressableButtonHoloLens2>().enabled = true;

                if (FindObjectOfType<TouchesCounter>() != null && FindObjectOfType<TouchesCounter>().isInsideAngle == false)
                    FindObjectOfType<TouchesCounter>().SetIsInsideAngle(true, Constants.LEFT_ANGLE);
            }
            else if (Mathf.Cos(_deltaSpace) >= Mathf.Cos(5.5f))
            {
                gameObject.GetComponent<Interactable>().enabled = true;
                gameObject.GetComponent<PressableButtonHoloLens2>().enabled = true;

                if (FindObjectOfType<TouchesCounter>() != null && FindObjectOfType<TouchesCounter>().isInsideAngle == false)
                    FindObjectOfType<TouchesCounter>().SetIsInsideAngle(true, Constants.RIGHT_ANGLE);
            }
            else
            {
                gameObject.GetComponent<Interactable>().enabled = false;
                gameObject.GetComponent<PressableButtonHoloLens2>().enabled = false;
                // Debug.Log("not near box");
                if (FindObjectOfType<TouchesCounter>() != null && FindObjectOfType<TouchesCounter>().isInsideAngle == true)
                    FindObjectOfType<TouchesCounter>().SetIsInsideAngle(false, Constants.ANGLE);
            }

            planetTransform.localPosition = new Vector3(initpos.x + x, initpos.y, initpos.z);
        }

    }
}
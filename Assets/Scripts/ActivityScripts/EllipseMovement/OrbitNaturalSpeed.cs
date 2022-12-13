using System.Collections;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class OrbitNaturalSpeed : MonoBehaviour
{

    public Transform orbitingObject;
    public AudioClip easyRhythm;
    public AudioClip mediumRhythm;
    public AudioClip difficultRhythm;
    public AudioClip easyMusic;
    public AudioClip mediumMusic;
    public AudioClip difficultMusic;
    private bool canMove = false;
    public Ellipse orbitPath;
    [Range(0F, 1F)]
    public float orbitProgress = 0f;
    public bool orbitActive = true;
    private float orbitPeriod;
    private Difficulty difficulty;
    private Vector3 initpos;


    // Use this for initialization
    void Awake()
    {
        if (orbitingObject == null)
        {
            orbitActive = false;
            return;
        }

        initpos = new Vector3(
            orbitingObject.localPosition.x,
            orbitingObject.localPosition.y - 0.5f,
            orbitingObject.localPosition.z
            );

        if (SceneChangerManager.Instance != null)
        {
            difficulty = SceneChangerManager.Instance.getDifficulty();
            if (difficulty == Difficulty.EASY)
            {
                orbitPeriod = 9.6f;
            }
            else if (difficulty == Difficulty.MEDIUM)
            {
                orbitPeriod = 8f;
            }
            else
            {
                orbitPeriod = 6.8572f;
            }
        }
        else
        {
            orbitPeriod = 8f;
        }

        StartSound();
        // SetOrbitingObjectPosition();
        StartCoroutine(AnimateOrbit());
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

    void SetOrbitingObjectPosition()
    {
        Vector2 orbitPos = orbitPath.Evaluate(orbitProgress);
        orbitingObject.localPosition =
            new Vector3(initpos.x + orbitPos.x, initpos.y + orbitPos.y, initpos.z);

        //Debug.Log("y neg"+orbitPos.y + "delta space " + orbitProgress);
        //Debug.Log("y pos"+orbitPos.y + "delta space " + orbitProgress);

        //if we want to restrict the area we have decrement the value of Sin
        if (Mathf.Cos(Mathf.Deg2Rad * 360f * orbitProgress) <= Mathf.Sin(-0.85f))
        {
            orbitingObject.gameObject.GetComponent<Interactable>().enabled = true;
            orbitingObject.gameObject.GetComponent<PressableButtonHoloLens2>().enabled = true;

            // Debug.Log("Bottom box");
            /* increment bottomCounter if the subject TOUCHES the planet, after the detection */
            if (FindObjectOfType<TouchesCounter>() != null && FindObjectOfType<TouchesCounter>().isInsideAngle == false)
                FindObjectOfType<TouchesCounter>().SetIsInsideAngle(true, Constants.BOTTOM_ANGLE);

        }
        else if (Mathf.Cos(Mathf.Deg2Rad * 360f * orbitProgress) >= Mathf.Sin(0.85f))
        {
            orbitingObject.gameObject.GetComponent<Interactable>().enabled = true;
            orbitingObject.gameObject.GetComponent<PressableButtonHoloLens2>().enabled = true;

            // Debug.Log("Top box");
            /* increment topCounter if the subject TOUCHES the planet, after the detection */
            if (FindObjectOfType<TouchesCounter>() != null && FindObjectOfType<TouchesCounter>().isInsideAngle == false)
                FindObjectOfType<TouchesCounter>().SetIsInsideAngle(true, Constants.TOP_ANGLE);
        }
        else
        {
            orbitingObject.gameObject.GetComponent<Interactable>().enabled = false;
            orbitingObject.gameObject.GetComponent<PressableButtonHoloLens2>().enabled = false;
            // Debug.Log("not near box");
            if (FindObjectOfType<TouchesCounter>() != null && FindObjectOfType<TouchesCounter>().isInsideAngle == true)
                FindObjectOfType<TouchesCounter>().SetIsInsideAngle(false, Constants.ANGLE);
        }

    }

    IEnumerator AnimateOrbit()
    {
        if (orbitPeriod < 0.1f)
        {
            orbitPeriod = 0.1f;
        }
        float frequence = 1f / orbitPeriod;

        while (orbitActive)
        {
            if (canMove)
            {
                orbitProgress += Time.deltaTime * frequence;
                orbitProgress %= 1f;
                SetOrbitingObjectPosition();
            }
            yield return null;
        }
    }

}

using System.Collections;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class OrbitNaturalSpeed : MonoBehaviour
{

    public Transform orbitingObject;
    [SerializeField]
    AudioClip[] audioClip;
    private bool isPlayed = false;
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
            Difficulty difficulty = SceneChangerManager.Instance.getDifficulty();
            if (difficulty == Difficulty.easy)
            {
                orbitPeriod = 9.6f;
            }
            else if (difficulty == Difficulty.medium)
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

        SetOrbitingObjectPosition();
        StartCoroutine(AnimateOrbit());
    }

    void SetOrbitingObjectPosition()
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

            orbitProgress += Time.deltaTime * frequence;
            orbitProgress %= 1f;
            SetOrbitingObjectPosition();
            yield return null;
        }
    }

}

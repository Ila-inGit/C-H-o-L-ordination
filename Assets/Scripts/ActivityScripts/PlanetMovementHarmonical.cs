using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;


public class PlanetMovementHarmonical : MonoBehaviour
{
    public Transform planetTransform;
    [SerializeField]
    AudioClip[] audioClip;
    private bool isPlayed = false;
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
            if (difficulty == Difficulty.easy)
            {
                speed = 0.589f;
            }
            else if (difficulty == Difficulty.medium)
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
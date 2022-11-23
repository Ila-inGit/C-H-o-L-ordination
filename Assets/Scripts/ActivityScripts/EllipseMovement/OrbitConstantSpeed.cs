using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class OrbitConstantSpeed : MonoBehaviour
{

    public Transform planetTransform;
    [SerializeField]
    AudioClip[] audioClip;
    private bool isPlayed = false;
    private float speed;
    private Difficulty difficulty;

    private const float A = 0.75f;
    private const float B = 0.5f;
    private float _x, _y, _deltaSpace;
    private Vector3 initpos;
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
                speed = 0.6545f;
            }
            else if (difficulty == Difficulty.medium)
            {
                speed = 0.7854f;
            }
            else
            {
                speed = 0.9163f;
            }
        }
        else
        {
            speed = 0.7854f;
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
        //constant speed
        _deltaSpace += Time.deltaTime * -speed;
        _x = (A * Mathf.Cos(_deltaSpace));
        _y = B * Mathf.Sin(_deltaSpace);
        planetTransform.localPosition = new Vector3(initpos.x + _x, initpos.y + _y, initpos.z);

        //if we want to restrict the area we have increment the value of Sin
        if (Mathf.Sin(_deltaSpace) <= Mathf.Sin(-0.9f))
        {
            gameObject.GetComponent<Interactable>().enabled = true;
            gameObject.GetComponent<PressableButtonHoloLens2>().enabled = true;

            if (FindObjectOfType<TouchesCounter>() != null && FindObjectOfType<TouchesCounter>().isInsideAngle == false)
                FindObjectOfType<TouchesCounter>().SetIsInsideAngle(true, Constants.BOTTOM_ANGLE);
        }
        else if (Mathf.Sin(_deltaSpace) >= Mathf.Sin(0.9f))
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
                /// sara vero?????
                FindObjectOfType<TouchesCounter>().SetIsInsideAngle(false, Constants.ANGLE);
        }

    }
}
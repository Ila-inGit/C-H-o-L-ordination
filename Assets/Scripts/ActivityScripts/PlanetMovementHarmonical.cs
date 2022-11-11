using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;


public class PlanetMovementHarmonical : MonoBehaviour
{
    public Transform planetTransform;
    private Vector3 initpos;
    private float _deltaSpace;
    private float speed;


    private void Start()
    {
        initpos = new Vector3(
            planetTransform.localPosition.x - 0.75f,
            planetTransform.localPosition.y,
            planetTransform.localPosition.z);

        Difficulty difficulty = SceneChangerManager.Instance.getDifficulty();
        if (difficulty == Difficulty.easy)
        {
            speed = 0.52f;
        }
        else if (difficulty == Difficulty.medium)
        {
            speed = 0.72f;
        }
        else
        {
            speed = 0.92f;
        }
    }

    void FixedUpdate()
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
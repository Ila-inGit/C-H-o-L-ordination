using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class OrbitNaturalSpeed : MonoBehaviour
{

    public Transform orbitingObject;
    public Ellipse orbitPath;
    [Range(0F, 1F)]
    public float orbitProgress = 0f;
    public float orbitPeriod;
    public bool orbitActive = true;
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

        SetOrbitingObjectPosition();
        StartCoroutine(AnimateOrbit());
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

            orbitProgress += Time.deltaTime * frequence;
            orbitProgress %= 1f;
            SetOrbitingObjectPosition();
            yield return null;
        }
    }

}

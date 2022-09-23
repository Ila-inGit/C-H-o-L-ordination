using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
public class ActivationTut1 : MonoBehaviour
{

    [HideInInspector] public bool isActivate = false;

    public GameObject Tutorial1;

    public GameObject planet;

    public GameObject hand;

    public GameObject box;

    public GameObject Tutorial2;

    // Update is called once per frame
    void FixedUpdate()
    {
        GetActivationBoxOnLook();
    }
    void LogCurrentGazeTarget()
    {
        if (CoreServices.InputSystem.GazeProvider.GazeTarget)
        {
            Debug.Log("User gaze is currently over game object: "
                + CoreServices.InputSystem.GazeProvider.GazeTarget);
        }
    }

    public void GetActivationBoxOnLook()
    {
        if (!isActivate)
        {
            if (CoreServices.InputSystem.GazeProvider.GazeTarget)
            {
                if (CoreServices.InputSystem.GazeProvider.GazeTarget.tag == "ActivationTut1")
                {
                    isActivate = true;
                    Activate();
                }
            }

        }
    }


    public void Activate()
    {

        if (planet != null && hand != null && box != null)
        {
            planet.SetActive(true);
            hand.SetActive(true);
            box.SetActive(true);
        }

        Tutorial1.GetComponent<Animator>().SetBool("isActive", true);
    }
}

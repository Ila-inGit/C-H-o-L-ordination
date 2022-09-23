using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
public class ActivationTut3 : MonoBehaviour
{

    private bool isActivate = false;

    public GameObject Tutorial3;

    public GameObject Tutorial1;

    public GameObject ContinueButton;

    void FixedUpdate()
    {
        GetActivationBoxOnLook();
    }

    public void GetActivationBoxOnLook()
    {
        if (!isActivate)
        {
            if (CoreServices.InputSystem.GazeProvider.GazeTarget)
            {
                if (CoreServices.InputSystem.GazeProvider.GazeTarget.tag == "ActivationTut3")
                {
                    isActivate = true;
                    Activate();
                }
            }

        }
    }


    public void Activate()
    {
        Tutorial3.GetComponent<Animator>().SetBool("isActive", true);

        if (ContinueButton != null)
            ContinueButton.SetActive(true);
    }
}

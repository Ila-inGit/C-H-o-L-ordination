using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;

public class ActivationTut2 : MonoBehaviour
{

    private bool isActivate = false;

    public GameObject Tutorial2;

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
                bool isTut1Activated = FindObjectOfType<ActivationTut1>().isActivate;
                if (CoreServices.InputSystem.GazeProvider.GazeTarget.tag == "ActivationTut2" && isTut1Activated)
                {
                    isActivate = true;
                    Activate();
                }
            }

        }
    }


    public void Activate()
    {
        Tutorial2.GetComponent<Animator>().SetBool("isActive", true);
    }
}

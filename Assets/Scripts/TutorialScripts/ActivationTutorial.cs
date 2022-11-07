using UnityEngine;
using Microsoft.MixedReality.Toolkit;
public class ActivationTutorial : MonoBehaviour
{

    private bool isActive = false;

    public GameObject ContinueButton;
    public GameObject Tutorial;

    void FixedUpdate()
    {
        GetActivationBoxOnLook();
    }

    public void GetActivationBoxOnLook()
    {
        if (!isActive)
        {
            if (CoreServices.InputSystem.GazeProvider.GazeTarget)
            {
                if (CoreServices.InputSystem.GazeProvider.GazeTarget.tag == Constants.ACTIVATION_TUTORIAL)
                {
                    isActive = true;
                    Activate();
                }
            }

        }
    }


    public void Activate()
    {
        Tutorial.GetComponent<Animator>().SetBool("isActive", true);

        if (ContinueButton != null)
            ContinueButton.SetActive(true);
    }
}

using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;

public class MakePlanetInteractableTutorial : MonoBehaviour
{

    public GameObject interactablePlanet;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == Constants.PLANET)
        {
            // Debug.Log("making interactable by: " + gameObject.tag);
            interactablePlanet.GetComponent<Interactable>().enabled = true;
            interactablePlanet.GetComponent<PressableButtonHoloLens2>().enabled = true;
            FindObjectOfType<StartTimer>().StartTimerForClick(gameObject.tag);

        }
    }



    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == Constants.PLANET)
        {
            // Debug.Log("making not interactable");
            interactablePlanet.GetComponent<Interactable>().enabled = false;
            interactablePlanet.GetComponent<PressableButtonHoloLens2>().enabled = false;
            FindObjectOfType<ChangeSpriteOnTouch>().ResetMesh();
            StartCoroutine(Wait());
            FindObjectOfType<StartTimer>().ResetTimer();

        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
    }
}

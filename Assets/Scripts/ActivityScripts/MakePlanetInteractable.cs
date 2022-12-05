using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;

public class MakePlanetInteractable : MonoBehaviour
{

    public GameObject interactablePlanet;

    public StartTimer timer;

    public float time = 0f;
    private bool secondaBox = false;
    private bool done = false;


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == Constants.PLANET)
        {

            if (FindObjectOfType<TouchesCounter>() != null)
                FindObjectOfType<TouchesCounter>().SetIsInsideBox(true, gameObject.tag);

            timer.StartTimerForClick(gameObject.tag);


            if (!secondaBox && !done)
            {
                secondaBox = true;
            }
            else if (secondaBox && !done)
            {
                done = true;
            }

        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Planet")
        {

            FindObjectOfType<ChangeSpriteOnTouch>().ResetMesh();
            StartCoroutine(Wait());
            if (FindObjectOfType<TouchesCounter>() != null)
                FindObjectOfType<TouchesCounter>().SetIsInsideBox(false, gameObject.tag);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        timer.ResetTimer();
    }
}

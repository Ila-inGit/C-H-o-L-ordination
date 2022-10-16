using UnityEngine;
using System.Threading.Tasks;

public class DisplayStartButton : MonoBehaviour
{
    private bool isInside = false;
    private bool instantiated = false;
    [SerializeField]
    GameObject startButton;

    public async void startCountdown()
    {
        await Task.Delay(600);
        // set active the prefabof the start button after a certain time
        if (isInside)
        {
            startButton.SetActive(true);
            instantiated = true;
            // initialize the camera position when it is inside the marker
            DataCollector.Instance.addToFileCamera(Camera.main.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!instantiated)
        {
            if (other.gameObject.tag == Constants.MAIN_CAMERA_TAG)
            {
                isInside = true;
                startCountdown();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!instantiated)
        {
            if (other.gameObject.tag == Constants.MAIN_CAMERA_TAG) { isInside = false; }
        }
    }
}

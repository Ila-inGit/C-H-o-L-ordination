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
        await Task.Delay(1000);
        // set active the prefabof the start button after a certain time
        if (isInside)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            // initialize the camera position when it is inside the marker
            DataCollector.Instance.addToFileCamera(cameraPos);

            startButton.transform.position = new Vector3(
            startButton.transform.position.x + cameraPos.x,
            startButton.transform.position.y + cameraPos.y,
            startButton.transform.position.z + cameraPos.z);

            startButton.SetActive(true);
            instantiated = true;

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

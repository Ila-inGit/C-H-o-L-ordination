using UnityEngine;
using System.Collections;

public class DisplayStartButton : MonoBehaviour
{
    private bool isInside = false;
    private bool instantiated = false;

    [SerializeField]
    GameObject startButton;
    [SerializeField]
    ParticleSystem particles;
    [SerializeField]
    AudioClip startClip;
    [SerializeField]
    AudioClip retryClip;

    IEnumerator startCountdown()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(10);
        // set active the prefabof the start button after a certain time
        if (isInside)
        {
            // magari da spostare sul scene manager controller
            Vector3 cameraPos = DataCollector.Instance.retriveCameraPositionFromFile();
            float cameraHeight = Camera.main.transform.position.y;
            cameraPos.y = cameraHeight;
            DataCollector.Instance.addCameraPositionToFile(cameraPos);
            Quaternion cameraAngle = DataCollector.Instance.retriveCameraAngleFromFile();

            startButton.transform.SetPositionAndRotation(cameraPos, cameraAngle);

            yield return new WaitForSeconds(2);

            startButton.transform.position = new Vector3(
                startButton.transform.position.x,
                startButton.transform.position.y,
                startButton.transform.position.z);

            startButton.SetActive(true);
            instantiated = true;
            particles.Stop();
            if (startClip != null)
            {
                SoundManager.Instance.Playsound(startClip);
            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SceneChangerManager.Instance.isInitialized())
        {
            if (!instantiated)
            {
                if (other.gameObject.tag == Constants.MAIN_CAMERA_TAG)
                {
                    isInside = true;
                    StartCoroutine(startCountdown());
                }
            }
        }
        else
        {
            if (retryClip != null)
            {
                SoundManager.Instance.Playsound(retryClip);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp : MonoBehaviour
{

    private static StartUp instance;

    [HideInInspector] private Vector3 cameraInitPosition;

    [SerializeField] private GameObject mark;

    public static StartUp Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<StartUp>();
            return instance;
        }
    }

    public void setPositionCamera(Transform fixedPosition)
    {
        cameraInitPosition = fixedPosition.position;
    }

    public void SetMarkActive()
    {
        if (mark != null && cameraInitPosition != null)
        {
            cameraInitPosition = DataCollector.Instance.retriveCameraFromFile();
            mark.transform.position = new Vector3(
                cameraInitPosition.x,
                cameraInitPosition.y + 0.3f,
                cameraInitPosition.z
            );
            mark.SetActive(true);
        }
    }

}

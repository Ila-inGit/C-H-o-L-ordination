using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionInit : MonoBehaviour
{
    public Transform myTransform;
    public float offsetCamera;


    private void Start()
    {
        if (DataCollector.Instance != null)
        {
            Vector3 cameraPos = DataCollector.Instance.retriveCameraPositionFromFile();
            Quaternion cameraAngle = DataCollector.Instance.retriveCameraAngleFromFile();

            myTransform.SetPositionAndRotation(cameraPos, cameraAngle);


            myTransform.position = new Vector3(
                myTransform.position.x,
                myTransform.position.y + offsetCamera,
                myTransform.position.z);
        }
    }

}

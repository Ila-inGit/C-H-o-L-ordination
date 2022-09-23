using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionInit : MonoBehaviour
{
    public Transform myTransform;
    private Vector3 cameraPos;
    public float offsetCamera;


    private void Start()
    {
        cameraPos = DataCollector.Instance.retriveCameraFromFile();

        myTransform.position = new Vector3(
            myTransform.position.x + cameraPos.x,
            myTransform.position.y + offsetCamera + cameraPos.y,
            myTransform.position.z + cameraPos.z);
    }

}

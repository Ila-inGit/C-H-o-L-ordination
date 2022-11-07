using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{

    public Transform handTransform;
    public float speed;
    private float offsetCamera;
    private Vector3 initpos;
    private Vector3 cameraPos;
    public float initYposHand;
    public float a = -0.12f;

    private void Start()
    {
        if (FindObjectOfType<OrbitConstantSpeed>() || FindObjectOfType<OrbitNaturalSpeed>())
            offsetCamera = -0.05f + initYposHand;

        initpos = new Vector3(
          handTransform.localPosition.x,
          handTransform.localPosition.y + offsetCamera,
          handTransform.localPosition.z
        );

        // handTransform.position = initpos;

    }
    void FixedUpdate()
    {
        // the two values can be changed to make the trajectory change
        float y = (Mathf.PingPong(Time.time * speed, 1) * a) / 4;
        handTransform.localPosition = new Vector3(initpos.x, y + initpos.y, initpos.z);
    }
}

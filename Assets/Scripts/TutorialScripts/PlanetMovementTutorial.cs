using UnityEngine;

public class PlanetMovementTutorial : MonoBehaviour
{
    public Transform planetTransform;
    private Vector3 initpos;
    public float speed;


    private void Awake()
    {
        initpos = new Vector3(
            planetTransform.localPosition.x + 0.3f,
            planetTransform.localPosition.y,
            planetTransform.localPosition.z);
    }

    void FixedUpdate()
    {
        // the two values can be changed to make the trajectory change
        float x = Mathf.PingPong(Time.time * speed, 1) * 0.6f - 0.3f;

        planetTransform.localPosition =
            new Vector3(initpos.x + x, initpos.y, initpos.z);
    }
}
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EllipseRenderer : MonoBehaviour
{
    private LineRenderer lr;
    [Range(3, 36)] public int segments;
    public Ellipse ellipse;

    private Transform cameraPos;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        cameraPos.position = DataCollector.Instance.retriveCameraFromFile();
        CalculateEllipse();
    }

    void CalculateEllipse()
    {
        Vector3[] points = new Vector3[segments + 1];
        for (int i = 0; i < segments; i++)
        {
            Vector2 position2D = ellipse.Evaluate((float)i / (float)segments);
            points[i] = new Vector3(position2D.x, position2D.y + cameraPos.position.y, 0f);
        }

        points[segments] = points[0];
        lr.positionCount = segments + 1;
        lr.SetPositions(points);
    }

    private void OnValidate()
    {
        if (Application.isPlaying && lr != null)
        {
            CalculateEllipse();
        }
    }
}

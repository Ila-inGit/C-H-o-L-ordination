using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class FigureEightRenderer : MonoBehaviour
{
    private LineRenderer lr;
    private float _time;
    public float speed;
    
    [Range(3, 36)] public int segments;
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        CalculateFigureEight();
    }

    void CalculateFigureEight()
    {
        Vector3[] points = new Vector3[segments + 1];
        for (int i = 0; i < segments; i++)
        {
            _time += Time.deltaTime * speed;
            var x =  Mathf.Cos(_time);
            var y =  Mathf.Sin(2*_time)/2;
            points[i] = new Vector3(x, y, 0f);
        }

        points[segments] = points[0];
        lr.positionCount = segments + 1;
        lr.SetPositions(points);
    }

    private void OnValidate()
    {
        if (Application.isPlaying && lr!= null)
        {
            CalculateFigureEight();
        }
    }
}


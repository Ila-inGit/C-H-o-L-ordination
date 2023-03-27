using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ellipse
{
    public float xAxis;
    public float yAxis;
    public float increment;
    public float increment_inverted;
    public bool isInvertedPart;

    public Ellipse(float xAxis, float yAxis, float increment, float increment_inverted)
    {
        this.xAxis = xAxis;
        this.yAxis = yAxis;
        this.increment = increment;
        this.increment_inverted = increment_inverted;
    }

    public Vector2 Evaluate(float t)// beta da 0 a 1
    {

        float angle = Mathf.Deg2Rad * 360f * t;// da 0 a 2*pi
        float E, E_invertita, x, y, x_invertita, y_invertita = 0.0f;

        if (!isInvertedPart) // primo
        {
            E = 2 * Mathf.Atan((xAxis / yAxis) * Mathf.Tan(angle + increment));
            x = Mathf.Sin(E) * xAxis;
            y = Mathf.Cos(E) * yAxis;

            if (y < 0)
            {
                isInvertedPart = true;
                increment_inverted = increment + 0.4f;
            }
            return new Vector2(x, y);
        }
        else // secondo
        {
            E_invertita = 2 * Mathf.Atan((yAxis / xAxis) * Mathf.Tan(angle + increment_inverted));
            x_invertita = Mathf.Sin(E_invertita) * xAxis;
            y_invertita = Mathf.Cos(E_invertita) * yAxis;
            if (y_invertita > 0)
            {
                increment = increment_inverted + 0.4f;
                isInvertedPart = false;
            }
            return new Vector2(x_invertita, y_invertita);
        }
    }
}
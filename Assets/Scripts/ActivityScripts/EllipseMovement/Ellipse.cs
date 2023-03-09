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

    // public Vector2 Evaluate(float t)// beta da 0 a 1
    // {

    //     float angle = Mathf.Deg2Rad * 360f * t;// da 0 a 2*pi
    //     float E, E_invertita, x, y, x_invertita, y_invertita = 0.0f;

    //     E = 2 * Mathf.Atan((xAxis / yAxis) * Mathf.Tan(angle));
    //     E_invertita = 2 * Mathf.Atan((yAxis / xAxis) * Mathf.Tan(angle + 0.4f));
    //     x = Mathf.Sin(E) * xAxis;
    //     y = Mathf.Cos(E) * yAxis;
    //     x_invertita = Mathf.Sin(E_invertita) * xAxis;
    //     y_invertita = Mathf.Cos(E_invertita) * yAxis;

    //     if (y > 0)
    //     {
    //         isInvertedPart = true;
    //         if (!firstLap)
    //         {
    //             E = 2 * Mathf.Atan((xAxis / yAxis) * Mathf.Tan(angle + 0.8f));
    //             x = Mathf.Sin(E) * xAxis;
    //             y = Mathf.Cos(E) * yAxis;
    //         }
    //         return new Vector2(x, y);
    //     }
    //     else if (!isInvertedPart && y < 0)
    //     {
    //         E = 2 * Mathf.Atan((xAxis / yAxis) * Mathf.Tan(angle + 0.8f));
    //         x = Mathf.Sin(E) * xAxis;
    //         y = Mathf.Cos(E) * yAxis;

    //         return new Vector2(x, y);
    //     }
    //     else if (y > 0 && isInvertedPart)
    //     {
    //         return new Vector2(x_invertita, y_invertita);
    //     }
    //     else
    //     {
    //         firstLap = false;
    //         if (y_invertita < 0)
    //         {
    //             isInvertedPart = true;
    //         }
    //         else
    //         {
    //             isInvertedPart = false;
    //         }
    //         return new Vector2(x_invertita, y_invertita);
    //     }
    // }

    // public Vector2 Evaluate(float t)// beta da 0 a 1
    // {

    //     float angle = Mathf.Deg2Rad * 360f * t;// da 0 a 2*pi
    //     float E, E_invertita, x, y, x_invertita, y_invertita = 0.0f;

    //     E = 2 * Mathf.Atan((xAxis / yAxis) * Mathf.Tan(angle));
    //     E_invertita = 2 * Mathf.Atan((yAxis / xAxis) * Mathf.Tan(angle));
    //     x = Mathf.Sin(E) * xAxis;
    //     y = Mathf.Cos(E) * yAxis;
    //     y_invertita = Mathf.Cos(E_invertita) * yAxis;
    //     //Debug.Log(y + " inv " + y_invertita);
    //     if (y > 0 && !thirdSector) // primo
    //     {
    //         isInvertedPart = true;
    //         if (!firstLap)
    //         {
    //             E = 2 * Mathf.Atan((xAxis / yAxis) * Mathf.Tan(angle + increment));
    //             x = Mathf.Sin(E) * xAxis;
    //             y = Mathf.Cos(E) * yAxis;
    //         }
    //         Debug.Log(y + " post aumento " + " primo");
    //         if (y < 0)
    //         {
    //             thirdSector = true;
    //             increment_inverted = increment + 0.4f;

    //         }

    //         return new Vector2(x, y);
    //     }
    //     else if (!isInvertedPart && y < 0 && !thirdSector) // terzo
    //     {
    //         E = 2 * Mathf.Atan((xAxis / yAxis) * Mathf.Tan(angle + increment));
    //         x = Mathf.Sin(E) * xAxis;
    //         y = Mathf.Cos(E) * yAxis;
    //         Debug.Log("secondo");

    //         return new Vector2(x, y);
    //     }
    //     else if (thirdSector || (y < 0)) // SECONDO
    //     {
    //         firstLap = false;
    //         E_invertita = 2 * Mathf.Atan((yAxis / xAxis) * Mathf.Tan(angle + increment_inverted));
    //         x_invertita = Mathf.Sin(E_invertita) * xAxis;
    //         y_invertita = Mathf.Cos(E_invertita) * yAxis;
    //         if (y_invertita < 0)
    //         {
    //             isInvertedPart = true;
    //         }
    //         else
    //         {
    //             increment = increment_inverted * 2;
    //             isInvertedPart = false;
    //             thirdSector = false;
    //         }
    //         Debug.Log("terzo");
    //         return new Vector2(x_invertita, y_invertita);
    //     }
    //     else
    //     {
    //         return new Vector2(x, y);
    //     }
    // }

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
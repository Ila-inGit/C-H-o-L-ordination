using UnityEngine;
using TMPro;
using System;

public class PointsCounter : MonoBehaviour
{

    public TMP_Text points;
    private float totalPoints = 0;


    private void Awake()
    {
        points.text = (0).ToString();
    }

    public void UpdatePoints(float pointsToAdd)
    {
        totalPoints += pointsToAdd;
        points.text = "";
        points.text = totalPoints.ToString();
    }
}

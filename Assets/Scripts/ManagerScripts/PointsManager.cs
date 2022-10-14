using UnityEngine;
using System.Collections.Generic;

public class PointsManager : MonoBehaviour
{
    private static PointsManager instance;
    private int totalPoints;

    public static PointsManager Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<PointsManager>();
            return instance;
        }
    }

    private void Start()
    {
        totalPoints = 0;
    }

    public void UpdatePoints(int pointsToAdd)
    {
        totalPoints += pointsToAdd;
    }

    public List<bool> GetStars()
    {
        if (totalPoints < 50)
        {
            return new List<bool> { false, false, true };
        }
        if (totalPoints >= 50 && totalPoints < 100)
        {
            return new List<bool> { false, true, true };
        }
        else
        {
            return new List<bool> { true, true, true };
        }

    }

    public void ResetPoints()
    {
        totalPoints = 0;
    }

}
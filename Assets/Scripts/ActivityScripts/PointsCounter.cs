using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Threading.Tasks;

public class PointsCounter : MonoBehaviour
{

    public TMP_Text pointLeftNumber;
    public TMP_Text pointCenterNumber;
    public TMP_Text pointRightNumber;
    private int totalPoints = 13;
    private bool active = false;

    private string totalPointsString;

    private void Awake()
    {
        pointLeftNumber.text = (0).ToString();
        pointCenterNumber.text = (0).ToString();
        pointRightNumber.text = (0).ToString();
    }

    public void UpdatePoints(int pointsToAdd)
    {
        totalPoints += pointsToAdd;
    }

    public async void Animate()
    {
        divideInDigits();
        displayPointsRand(pointLeftNumber, totalPointsString[0]);
        await Task.Delay(10);
        displayPointsRand(pointCenterNumber, totalPointsString[1]);
        await Task.Delay(10);
        displayPointsRand(pointRightNumber, totalPointsString[2]);
    }

    public async void displayPointsRand(TMP_Text pointsText, char digit)
    {
        for (int i = 0; i < 10; i++) //Number of rolls before showing final
        {
            await Task.Delay(100);
            pointsText.text = "";
            pointsText.text = Random.Range(0, 9).ToString();
        }
        pointsText.text = "";
        pointsText.text = digit.ToString();
    }

    public void divideInDigits()
    {
        totalPointsString = totalPoints.ToString().PadLeft(3, '0');
    }
}

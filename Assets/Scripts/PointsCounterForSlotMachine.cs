using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Threading.Tasks;

public class PointsCounterForSlotMachine : MonoBehaviour
{

    public MeshRenderer pointLeftSprite;
    public MeshRenderer pointCenterSprite;
    public MeshRenderer pointRightSprite;
    public List<Material> materials;
    private int totalPoints = 90;

    private string totalPointsString;

    private void Awake()
    {
        pointLeftSprite.material = materials[0];
        pointRightSprite.material = materials[1];
        pointCenterSprite.material = materials[2];

    }

    public void UpdatePoints(int pointsToAdd)
    {
        totalPoints += pointsToAdd;
    }

    public async void Animate()
    {
        List<bool> stars = getStars();
        int finalMatIndex = Random.Range(0, materials.Count - 1);

        displayMaterialRand(pointLeftSprite, stars[0], finalMatIndex);
        await Task.Delay(100);
        displayMaterialRand(pointCenterSprite, stars[2], finalMatIndex);
        await Task.Delay(100);
        displayMaterialRand(pointRightSprite, stars[1], finalMatIndex);
    }

    public async void displayMaterialRand(MeshRenderer renderer, bool isStar, int finalIndex)
    {
        for (int i = 0; i < 30; i++) //Number of rolls before showing final
        {
            await Task.Delay(100 + i * 5);
            int index = Random.Range(0, materials.Count - 1);
            renderer.material = materials[index];
        }
        if (isStar)
        {
            renderer.material = materials[finalIndex];
        }
    }

    public List<bool> getStars()
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


}

using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

// Put this on the slot machine object prefab 
public class PointsCounterForSlotMachine : MonoBehaviour
{

    public MeshRenderer pointLeftSprite;
    public MeshRenderer pointCenterSprite;
    public MeshRenderer pointRightSprite;
    public List<Material> materials;

    private void Awake()
    {
        pointLeftSprite.material = materials[0];
        pointRightSprite.material = materials[1];
        pointCenterSprite.material = materials[2];
    }

    public async void Animate()
    {
        if (PointsManager.Instance != null)
        {
            List<bool> stars = PointsManager.Instance.GetStars();
            int finalMatIndex = Random.Range(0, materials.Count - 1);

            displayMaterialRand(pointLeftSprite, stars[0], finalMatIndex);
            await Task.Delay(100);
            displayMaterialRand(pointCenterSprite, stars[2], finalMatIndex);
            await Task.Delay(100);
            displayMaterialRand(pointRightSprite, stars[1], finalMatIndex);

            PointsManager.Instance.ResetPoints();
        }
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

}

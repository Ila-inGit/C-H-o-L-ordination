using UnityEngine;
using System.Threading.Tasks;

public class InstantiateMarkerPrefab : MonoBehaviour
{

    // Start is called before the first frame update
    async void Start()
    {
        await Task.Delay(1000);
        gameObject.GetComponent<Microsoft.MixedReality.Toolkit.Experimental.SceneUnderstanding.SceneUnderstandingController>()
            .InstantiateMarkerOnFloor();
        // TODO(ilaria): forse da trovare un modo per stoppare il mapping
    }
}
